from collections import namedtuple
import io
import browser_settings
from browser_settings import IO_subsystem as io_subs
from browser_settings import requests

proxy = namedtuple('proxy_obj', ['ip', 'port', 'protocol'])
page_number = 1
pages_count = 1
proxy_list = []
proxy_save_file_name = ''


def getProxyList(from_site_url, browser, file_format, data_format, pagination_container_t, pagination_container_c, table_c, from_file_name=None, file_for_save_list='proxies.txt'):

    proxy_item_settings = []
    global proxy
    global page_number
    global pages_count
    global proxy_list
    if from_site_url != None:
        browser.get(from_site_url)
        web_page = browser.page_source
        parser = browser_settings.BeautifulSoup(web_page, "html.parser")
        if pagination_container_t != None and pagination_container_c != None:
            pages_count = len(parser.find(pagination_container_t, {
                "class": pagination_container_c}).find_all())
        else:
            pages_count = 1
        table = parser.find("table", {"class": table_c})
        links = table.find_all(["tr", "td"])
        links_dict = {}
        tr_key = 0
        for link in links:
            if link.name == 'tr':

                links_dict[tr_key] = []
                tr_key += 1
            else:
                links_dict[tr_key-1].append(link)
        for proxy_items in links_dict.values():
            proxy_item_settings = []
            for item in proxy_items:
                if not item.find('a'):
                    proxy_item_settings.append(item.text)
                else:
                    proxy_item_settings.append((item.find('a').text))
            if len(proxy_item_settings) >= 3:
                if browser_settings.re.fullmatch("[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}", proxy_item_settings[0]):
                    proxy_list.append(
                        proxy(proxy_item_settings[0], proxy_item_settings[1], proxy_item_settings[2]))

        if page_number < pages_count-1 and requests.get(from_site_url+'&page='+str(page_number)).status_code != 404:
            page_number += 1
            getProxyList(from_site_url+'&page='+str(page_number), browser, file_format,
                         data_format, pagination_container_t, pagination_container_c, table_c)
        else:
            io_subs.write_to_file(
                proxy_list, file_for_save_list, data_format, file_format)
            return proxy_list
    if from_file_name != None:
        if io_subs.os.path.exists(from_file_name):
            for proxy_item_settings in io_subs.read_from_file(from_file_name, data_format, file_format):
                if browser_settings.re.fullmatch("[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}", proxy_item_settings[0]):
                    proxy_list.append(
                        proxy(proxy_item_settings[0], proxy_item_settings[1], proxy_item_settings[2]))
                else:
                    proxy_list = None
