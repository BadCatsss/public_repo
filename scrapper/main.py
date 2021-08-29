import os
from sre_constants import FAILURE


import random
import time
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.common.by import By
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.remote import webelement

from browser_settings import requests

import subprocess
import node_obj
import http.server
import socketserver

import db_driver
from datetime import datetime
import get_proxy
from get_proxy import browser_settings
from node_obj import *

log_file_name='items.txt'

class Web_parser:
    # containers
    products_categories_sublinks = None
    start_url = ''
    containers_hub = []
    item_url_class = []
    href_text_list = []
    url_class_list = []
    find_children_list = []
    forbidden_sub_utls = {}
    forbidden_href_class = []
    site_start_url = ''
    # loacal server settings
    isLocalMode = False,
    localhost_page = '',
    local_port = ''
    server_sub_proc = None
    visited_links = []
    is_find_item = False
    items_list = list()
    price_element_id = ''
    timeout_max_val=0
    large_timeout=0
    count_to_large_timeout=10
    count_to_time_out_event=0

    containers_hub_pointer = 0
    href_text_list_pointer = 0
    url_class_list_pointer = 0
    children_list_pointer = 0

    get_proxy.browser_settings.create_browser_instance('firefox')
    from browser_settings import browser_instance as browser
    # for scrappy proxy list from site
    # get_proxy.getProxyList('https://scrapingant.com/free-proxies/', browser,
    # 'txt', 'csv', None, None, 'proxies-table', None, 'proxies1.txt')
    # for scrappy proxy list from file
    get_proxy.getProxyList(None, browser, 'txt', 'csv',
                           None, None, None, 'proxies.txt')
    from get_proxy import proxy_list as pr_lis
    if pr_lis != None:
        get_proxy.browser_settings.update_proxy_settings(pr_lis)
    parser = None
    page_src = ''
    path_to_json = ''

    

    def __init__(self, path_to_json, site_url, db_schema, db_driver_name, isLocalMode=False, localhost_page=None, port=None):
        Web_parser.products_categories_sublinks = set()
        Web_parser.isLocalMode = isLocalMode,
        Web_parser.localhost_page = localhost_page,
        Web_parser.local_port = port

        Web_parser.start_url = site_url
        Web_parser.containers_hub = [self.products_categories_sublinks, dict()]

        Web_parser.path_to_json = path_to_json
        json_obj = get_proxy.io_subs.parse_json_object(
            self.path_to_json, self.start_url)

        Web_parser.containers_hub = [self.products_categories_sublinks, dict()]
        Web_parser.item_url_class = json_obj["final_item_url_class"]
        Web_parser.href_text_list = json_obj["href_text_list"]
        Web_parser.url_class_list = json_obj["url_class_list"]
        Web_parser.find_children_list = json_obj["find_children_list"]
        Web_parser.forbidden_sub_utls = json_obj["forbidden_sub_utls"]
        Web_parser.forbidden_href_class = json_obj["forbidden_href_class"]
        Web_parser.start_url = json_obj["site_start_url"]
        Web_parser.price_element_id = json_obj["price_value_unique_part"]
        Web_parser.timeout_max_val=json_obj["timeout_max_val"]
        Web_parser.large_timeout=json_obj["large_timeout"]
        Web_parser.count_to_large_timeout=json_obj["count_to_large_timeout"]
        Web_parser.count_to_time_out_event=0

        root_hrml_obj = node_obj.html_node_obj(self.start_url, self.containers_hub[self.containers_hub_pointer],
                                               self.href_text_list[self.href_text_list_pointer], self.url_class_list[self.url_class_list_pointer], None, None)
        db_driver.create_db(db_schema, db_driver_name)
        self.get_sub_links(root_hrml_obj)
       # self.browser.close()

    def add_to_container(value, container, key=""):
        wrong_keys = ["", "/"]
        if type(container) is list:
            container.append(value)
        elif type(container) is set:
            container.add(value)
        elif key not in wrong_keys and type(container) is dict:
            if type(key) is str:
                if key not in container.keys():
                    container.update({key: [value]})
                else:
                    container[key].append(value)

    def check_sunlink(link, node_obj):
        sublink_is_correct = False

        if node_obj.find_href_text != "":
            if browser_settings.re.fullmatch(node_obj.find_href_text, str(link.text)) != None:
                sublink_is_correct = True
            else:
                sublink_is_correct = False
        else:
            sublink_is_correct = True

            found_sunlink_class = browser_settings.re.fullmatch(
                ".*"+node_obj.find_url_class+".*", str(link))

            if found_sunlink_class != None and node_obj.find_url_class not in Web_parser.forbidden_href_class and node_obj.find_url_class in Web_parser.url_class_list:
                found_sunlink_class = browser_settings.re.sub(
                    "\<a ?(href|class) ?= ?\"", "", found_sunlink_class.group(0))
                search_pattern = ".*("

                for item_class in Web_parser.item_url_class:
                    search_pattern += item_class+'|'

                search_pattern = search_pattern[:-1]

                search_pattern += ").*"
                if browser_settings.re.search(search_pattern, found_sunlink_class) != None:
                    Web_parser.is_find_item = True
                else:
                    Web_parser.is_find_item = False
                    # db_driver.add_to_db('items',{'item_hash':get_proxy.io_subs.getItemHash(),'last_update_date':datetime.now()})

                sublink_is_correct = True
                # get_proxy.io_subs.write_to_db(
                #     "", "db_schema.json", "sqlite3")  # for test

            else:
                sublink_is_correct = False

            child_was_found = False

            if node_obj.find_children_list != None:
                for child_node_el in node_obj.find_children_list:
                    for key, value in child_node_el.items():
                        el = link.findChildren(key, {'class': value})
                        if el != None:  # TO_DO
                            child_was_found = True
                            break

                if child_was_found:
                    sublink_is_correct = True

            else:
                sublink_is_correct = True

        return sublink_is_correct

    def start_local_server(localhost_page, port):
        global server_sub_proc
        server_sub_proc = subprocess.Popen(['python3', '-u', '-m', 'http.server', port],
                                           stdout=subprocess.PIPE,
                                           stderr=subprocess.STDOUT)
        server_sub_proc.wait()

        # command = 'cmd python -m http.server '+port
        # # On Windows:
        # # command = 'cmd /c "python job.py"'
        # subprocess.call(command, shell=True)

    def check_captcha(page_src):
        if browser_settings.re.search("Защита от роботов", page_src) != None:
            WebDriverWait(Web_parser.browser, 10).until(EC.frame_to_be_available_and_switch_to_it(
                (By.CSS_SELECTOR, "iframe[name^='a-'][src^='https://www.google.com/recaptcha/api2/anchor?']")))
            WebDriverWait(Web_parser.browser, 10).until(EC.element_to_be_clickable(
                (By.XPATH, "//span[@id='recaptcha-anchor']"))).click()

            if browser_settings.re.search("Защита от роботов", page_src) != None:
                get_proxy.browser_settings.update_proxy_settings(
                    Web_parser.pr_lis)
                time.sleep(Web_parser.large_timeout)
                Web_parser.get_page(node_obj, Web_parser.isLocalMode,
                                    Web_parser.localhost_page, Web_parser.local_port)

    def get_page(node_obj, isLocalMode, localhost_page, port):

        global page_src
        global parser

        time.sleep(random.randint(1, Web_parser.timeout_max_val))
        Web_parser.browser.switch_to_default_content()

        if isLocalMode[0] is True:
            # START SERVER
            # WTF localhost_page is tuple ???!!!
            host = 'localhost:'+port+'/'+localhost_page[0]
            Web_parser.start_local_server(localhost_page, port)

            Web_parser.browser.get(host)
        else:
            try:
                Web_parser.browser.get(node_obj.start_search_url)
                page_src = Web_parser.browser.page_source

                parser = get_proxy.browser_settings.BeautifulSoup(
                    page_src, "html.parser")
            except Exception:
                return
    # main method

    def get_sub_links(self, node_obj):
        global containers_hub_pointer
        global href_text_list_pointer
        global url_class_list_pointer
        global children_list_pointer

        Web_parser.count_to_time_out_event+=1
        if Web_parser.count_to_time_out_event>=Web_parser.count_to_large_timeout:
            time.sleep(Web_parser.large_timeout)
            Web_parser.count_to_time_out_event=0

        

        if requests.get(node_obj.start_search_url).status_code == 404:
            # try get 'short' link
            if requests.get(Web_parser.start_url+Web_parser.get_sub_links.dict_container_key).status_code == 404:
                return

        Web_parser.get_page(node_obj, Web_parser.isLocalMode,
                            Web_parser.localhost_page, Web_parser.local_port)
        Web_parser.check_captcha(page_src)

        products_categories_items = parser.find_all("a")

        if not hasattr(Web_parser.get_sub_links, "dict_container_key"):
            Web_parser.get_sub_links.dict_container_key = ""
        if not hasattr(Web_parser.get_sub_links, "key_to_append"):
            Web_parser.get_sub_links.key_to_append = ""

        for link in products_categories_items:
            if Web_parser.check_sunlink(link, node_obj) == True:
                # (([^:\/?#]+):)?(\/\/([^\/?#]*))?([^?#]*)(\?([^#]*))?(#(.*))? [from:https://tools.ietf.org/html/rfc3986#appendix-B]
                # ^((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$
                # ((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)? [from:https://stackoverflow.com/questions/27745/getting-parts-of-a-url-regex]
                if browser_settings.re.search(r'href=(\"(.*?)\"\>)', str(link)) != None:
                    if browser_settings.re.search(r'((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?"\s]+)(.*)?(#[\w\-]+)?', "/"+str(link)).group(6).find("/") == -1:
                        node_obj.category_name = browser_settings.re.search(
                            r'((http[s]?|ftp):\/)?\/?([^:\/\s]+)((\/\w+)*\/)([\w\-\.]+[^#?"\s]+)(.*)?(#[\w\-]+)?', "/"+str(link)).group(6)
                        if browser_settings.re.search(r'^[^0-9]*$', node_obj.category_name) != None and ("/"+node_obj.category_name not in Web_parser.forbidden_sub_utls):
                            # FILL CONTAINER

                            Web_parser.add_to_container("/"+node_obj.category_name,
                                                        node_obj.node_container, Web_parser.get_sub_links.key_to_append)
                        if Web_parser.is_find_item is True:
                            if '.html' in node_obj.category_name:
                                Web_parser.items_list.append(
                                    node_obj.category_name)

        container_for_iteration = None

        def reinvoke_method(start_url):
            global containers_hub_pointer
            global href_text_list_pointer
            global url_class_list_pointer
            global children_list_pointer

            child_html_obj = html_node_obj(start_url,
                                           Web_parser.containers_hub[Web_parser.containers_hub_pointer-1],
                                           Web_parser.href_text_list[Web_parser.href_text_list_pointer-1],
                                           Web_parser.url_class_list[Web_parser.url_class_list_pointer-1],
                                           Web_parser.find_children_list[Web_parser.children_list_pointer-1], node_obj)
            Web_parser.get_sub_links(self, child_html_obj)

        def next_iter():
            global log_file_name
            with open(log_file_name, "a", encoding="utf-8") as items_file:
                for item in Web_parser.items_list:
                    url = Web_parser.start_url+'/'+item+'\n'
                    items_file.write(url)
                    item_price = db_driver.get_item_price(url,Web_parser.browser,Web_parser.price_element_id)
                    item_hash = get_proxy.io_subs.getItemHash(url)
                    db_driver.add_to_db('items', {
                                        'item_hash': item_hash,'item_raw_url':url, 'last_update_date': datetime.date(datetime.now())})
                    db_driver.add_to_db('prices', {
                                        'item_hash': item_hash, 'price': item_price, 'actual_at': datetime.date(datetime.now())})
                items_file.close()
            Web_parser.items_list.clear()
            print('was add new links to db\n')
            global visited_links

            if container_for_iteration != None:
                for key_val in container_for_iteration:
                    if key_val not in Web_parser.visited_links:
                        Web_parser.visited_links.append(key_val)
                        Web_parser.get_sub_links.dict_container_key = key_val

                        node_obj.start_search_url = Web_parser.start_url

                        if type(Web_parser.get_sub_links.dict_container_key) is not str:
                            for subitem in Web_parser.get_sub_links.dict_container_key:
                                Web_parser.get_sub_links.key_to_append = subitem
                                reinvoke_method(
                                    node_obj.start_search_url+subitem)
                        else:
                            Web_parser.get_sub_links.key_to_append = Web_parser.get_sub_links.dict_container_key
                            reinvoke_method(node_obj.start_search_url +
                                            Web_parser.get_sub_links.dict_container_key)

        if Web_parser.containers_hub_pointer <= len(Web_parser.containers_hub)-1:
            Web_parser.containers_hub_pointer += 1
        if Web_parser.href_text_list_pointer <= len(Web_parser.href_text_list)-1:
            Web_parser.href_text_list_pointer += 1
        if Web_parser.url_class_list_pointer <= len(Web_parser.url_class_list)-1:
            Web_parser.url_class_list_pointer += 1
        if Web_parser.children_list_pointer <= len(Web_parser.find_children_list)-1:
            Web_parser.children_list_pointer += 1

        if type(Web_parser.containers_hub[Web_parser.containers_hub_pointer-1]) is not dict:
            container_for_iteration = Web_parser.containers_hub[Web_parser.containers_hub_pointer-1]
        else:

            for sub_cut in Web_parser.containers_hub[Web_parser.containers_hub_pointer - 1].values():

                container_for_iteration = sub_cut
                next_iter()

        next_iter()
        node_obj.start_url = node_obj.start_search_url.replace(
            "/"+node_obj.category_name, "")


def main():
    scrappy_obj = Web_parser(
        'site_objects.json', 'https://prom.ua', 'db_schema.json', 'sqlite3')

    #scrappy_obj = Web_parser('site_objects.json', 'https://prom.ua',True,'test1.html','8000')
    # test
    # db_driver.read_from_db("items",
    #      "item_hash", "")
    # db_driver.add_to_db('items',{'item_hash':'123abc','last_update_date':'06.06.21'})
    # db_driver.update_db('items','item_hash','2222')


if __name__ == "__main__":
    main()
