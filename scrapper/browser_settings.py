import platform
from selenium import webdriver
from selenium.webdriver.firefox.firefox_binary import FirefoxBinary
from bs4 import BeautifulSoup
from fake_useragent import UserAgent
from collections import namedtuple
import IO_subsystem
import re
import requests

available_browsers = {'firefox': 1, 'chrome': 2, 'tor': 3}
browser_options = None
browser_op = None
ua = UserAgent()
user_agent = ua.random
browser_instance = None
browser_driver_location_path = ''
browser_binary_location_path = ''
browser_name = ''
proxy_list_position = 0

def recreate():
            global browser_instance
            if available_browsers[browser_name] == 1:
                browser_instance = webdriver.Firefox(
                    firefox_profile=browser_options, firefox_binary=browser_binary_location_path, executable_path=browser_driver_location_path, options=browser_op)
            elif available_browsers[browser_name] == 2:
                browser_instance = webdriver.Chrome(
                    browser_driver_location_path, chrome_options=browser_options)

def get_instance():
    global browser_driver_location_path
    global browser_binary_location_path
    global browser_name
    global browser_options
    global browser_op
    global browser_instance

    if browser_instance is None:
       recreate()
      

def set_instance_preferences(browser_name):
    global browser_options
    global browser_op

    browser_name = browser_name.lower()
    if browser_name in available_browsers.keys():
        if available_browsers[browser_name] == 1:
            from selenium.webdriver.firefox.options import Options
            browser_options = webdriver.FirefoxProfile()
            browser_options.set_preference('dom.webdriver.enabled', False)
            browser_options.set_preference("places.history.enabled", False)
            browser_options.set_preference(
                "privacy.clearOnShutdown.offlineApps", True)
            browser_options.set_preference(
                "privacy.clearOnShutdown.passwords", True)
            browser_options.set_preference(
                "privacy.clearOnShutdown.siteSettings", True)
            browser_options.set_preference(
                "privacy.sanitize.sanitizeOnShutdown", True)
            browser_options.set_preference("signon.rememberSignons", False)
            browser_options.set_preference("network.cookie.lifetimePolicy", 2)
            browser_options.set_preference("network.dns.disablePrefetch", True)
            browser_options.set_preference("network.http.sendRefererHeader", 0)
            browser_op = Options()
            browser_op.headless = True

        if available_browsers[browser_name] == 2:
            browser_options = webdriver.ChromeOptions()
            # для открытия headless-браузера
            browser_options.add_argument('headless')
            browser_options.add_argument(f'user-agent={user_agent}')
            browser_options.add_argument("enable-automation")
            browser_options.add_argument("--headless")
            browser_options.add_argument("--no-sandbox")
            browser_options.add_argument("--disable-extensions")
            browser_options.add_argument("--dns-prefetch-disable")
            browser_options.add_argument("--disable-gpu")


def create_browser_instance(browser_name_):

    global browser_driver_location_path
    global browser_binary_location_path
    global browser_name
    browser_name = browser_name_.lower()

    if platform.system() == 'Windows':
        if browser_name in available_browsers.keys():
            if available_browsers[browser_name] == 1:
                #gecko (firefox)
                browser_driver_location_path = 'web_drivers\\Windows\\geckodriver.exe'
                browser_binary_location_path = 'E:\\Firefox\\firefox.exe'
            elif available_browsers[browser_name] == 2:
                # chrome
                browser_driver_location_path = 'web_drivers\\Windows\\chromedriver.exe'
                browser_binary_location_path = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe"
            elif available_browsers[browser_name] == 3:
                # if need tor
                browser_driver_location_path = 'web_drivers\\Windows\\geckodriver.exe'
                browser_binary_location_path = r'E:\Tor\Tor Browser\Browser\firefox.exe'
        else:
            #gecko (firefox)
            browser_driver_location_path = 'web_drivers\\Windows\\geckodriver.exe'
            browser_binary_location_path = 'E:\\Firefox\\firefox.exe'

    if platform.system() == 'Linux':
        if browser_name.lower() in available_browsers.keys():
            if available_browsers[browser_name] == 1:
                #gecko (firefox)
                browser_driver_location_path = 'web_drivers/Linux/geckodriver'
                browser_binary_location_path = '/usr/lib/firefox-esr/firefox-bin'
            elif available_browsers[browser_name] == 2:
                # chrome
                browser_driver_location_path = 'web_drivers/Linux/chromedriver'
                browser_binary_location_path = "/usr/bin/google-chrome-stable"
            elif available_browsers[browser_name] == 3:
                # if need tor
                browser_driver_location_path = 'web_drivers\\Windows\\geckodriver.exe'
                browser_binary_location_path = r'sh -c "/home/badcatss/tor/Browser/start-tor-browser"'
            else:
             #gecko (firefox)
                browser_driver_location_path = 'web_drivers/Linux/geckodriver'
                browser_binary_location_path = ''

    set_instance_preferences(browser_name)
    get_instance()


def update_proxy_settings(proxy_list):
    global proxy_list_position
    global browser_options
    global browser_instance
    browser_instance=None
    if proxy_list_position >= len(proxy_list):
        proxy_list_position = 0
        if proxy_list!=None and  len(proxy_list) > 0:
            current_proxy_server = proxy_list[proxy_list_position]
            browser_options.set_preference("network.proxy.type", 1)
            browser_options.set_preference(
                "network.proxy."+current_proxy_server.protocol, current_proxy_server.ip)
            browser_options.set_preference(
                "network.proxy.http_port", current_proxy_server.port)
    get_instance()

    # set socks proxy
    # browser_options.set_preference("network.proxy.type", 1)
    # browser_options.set_preference("network.proxy.socks_version", 5)
    # browser_options.set_preference("network.proxy.socks", '127.0.0.1')
    # browser_options.set_preference("network.proxy.socks_port", 9150)
    # browser_options.set_preference("network.proxy.socks_remote_dns", True)
