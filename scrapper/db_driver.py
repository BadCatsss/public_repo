import json
import hashlib
import os
import sqlite3
from sys import path
import platform
from browser_settings import re

db_connection = None
schema = None
separator = ''
os_schema_path = ''
db_path = ''



def connect_to_db(schema):
    global db_connection
    db_connection = sqlite3.connect(schema[os_schema_path])


def get_path_by_os():
    global separator
    global os_schema_path
    global db_path

    if platform.system() == 'Windows':
        separator = '\\'
        os_schema_path = "db_file_path_windows"
        db_path = schema[os_schema_path]
    elif platform.system() == 'Linux':
        separator = '/'
        os_schema_path = "db_file_path_linux"
        db_path = schema[os_schema_path]


def create_db(db_schema, db_driver):

    global db_connection
    global schema
    global separator

    if db_schema != None and type(db_schema) is str and os.path.exists(db_schema) and os.path.isfile(db_schema):
        with open(db_schema, "r", encoding="utf-8") as read_file:
            schema = json.load(read_file)
            get_path_by_os()
        if schema["db_driver"] == db_driver:
            if os.path.exists(schema[os_schema_path]):
                if db_connection == None:
                    connect_to_db(schema)

                current_cursor = db_connection.cursor()
                db_tables = schema["tables"]
                for table in db_tables:
                    primary_key_list = list()
                    foreign_key_dict = dict()
                    foreign_key = ''
                    foreign_table = ''
                    foreign_table_attrib = ''
                    foreign_q_value = ''
                    primary_q_value = ''
                    atribs_list = list()

                    for table_keys in table["keys"]:
                        for k in table_keys.keys():
                            if k == "PK":
                                primary_key_list.append(table_keys[k])
                            if k == "FK":
                                for k in table_keys[k]:
                                    for k1 in k.keys():
                                        if k1 == 'key_attrib':
                                            foreign_key = k[k1]
                                        if foreign_key != '':
                                            if k1 == 'main_table':
                                                foreign_table = k[k1]
                                            if k1 == 'main_table_attrib':
                                                foreign_table_attrib = k[k1]
                                            foreign_q_value = 'FOREIGN KEY ('+foreign_key+') REFERENCES ' + \
                                                foreign_table + \
                                                '('+foreign_table_attrib+')'
                                        foreign_key_dict.update(
                                            foreign_key=foreign_q_value)

                    for table_attrib in table["attribs"]:
                        for k in table_attrib.keys():
                            attrib_val = table_attrib[k]
                            if k in primary_key_list:
                                primary_q_value = ' PRIMARY KEY AUTOINCREMENT NOT NULL'
                            else:
                                primary_q_value = ' '
                            atribs_list.append(
                                k + ' '+attrib_val+primary_q_value)

                    query = "CREATE TABLE IF NOT EXISTS " + \
                        table["table_name"]+" ( "
                    for atrib in atribs_list:
                        query += atrib+','
                    for fk in foreign_key_dict.values():
                        query += fk

                    if query[-1] == ',':
                        query = query[:-1]
                    query += ');'
                    current_cursor.execute(query)
                current_cursor.close()

            else:
                folder_path = ''

                db_full_path = db_path.split(separator)
                if len(db_full_path) == 1:
                    folder_path = ''
                else:
                    for path_part in range(len(db_full_path)-1):
                        folder_path += db_full_path[path_part]
                folder_path += separator
                if not os.path.exists(folder_path):
                    os.mkdir(folder_path)
                if not os.path.exists(folder_path+db_full_path[-1]):
                    f = open(folder_path+db_full_path[-1], "w")
                    f.close()
                read_file.close()
                create_db(db_schema, db_driver)


def check_params(table_name, attrib):
    create_db('db_schema.json', 'sqlite3')
    global db_connection
    global schema
    if db_connection != None:
        current_cursor = db_connection.cursor()
        current_cursor.execute(
            'SELECT name FROM sqlite_master WHERE type = "table"')
        tables_list = current_cursor.fetchall()
        table_exist = False
        for table in tables_list:
            if table_name in table[0]:
                current_cursor.close()
                table_exist = True

        if table_exist:
            # db_connection.close()
            # connect_to_db(schema)

            select_cursor = db_connection.cursor()
            select_cursor.execute(
                'select * from sqlite_master where type = "table" and name="'+table_name+'";')
            # select_cursor.execute('select * from '+table_name+';')
            res = select_cursor.fetchall()
            create_quary = (res[0])[-1]
            if attrib in create_quary:
                return True
    return False


def read_from_db(table_name, attrib, constraints=''):
    current_cursor = db_connection.cursor()
    if check_params(table_name, attrib):
        select_query = ''
        select_query = 'SELECT '+attrib+' FROM '+table_name+' '+constraints
        current_cursor.execute(select_query)
        return current_cursor.fetchall()
    return None


def update_db(table_name, attrib, atrib_new_val, constraints=''):
    create_db('db_schema.json', 'sqlite3')
    current_cursor = db_connection.cursor()
    if check_params(table_name, attrib):
        update_query = 'UPDATE '+table_name+' SET ' + \
            attrib+'='+atrib_new_val+' '+constraints
        current_cursor.execute(update_query)
        db_connection.commit()


def add_to_db(table_name, values_dict):
    create_db('db_schema.json', 'sqlite3')
    current_cursor = db_connection.cursor()
    insert_query = 'INSERT INTO '+table_name+' ('
    query_keys = ''
    query_values = ' VALUES ('
    for val in values_dict:
        if check_params(table_name, val):
            query_keys += val+','
            if type(values_dict[val]) != str:
                try:
                    query_values += '\''+values_dict[val].hexdigest()+'\''+','
                except Exception:
                    query_values += '\''+str(values_dict[val])+'\''+','
            else:
                query_values += '\''+values_dict[val]+'\''+','
    if query_keys[-1] == ',':
        query_keys = query_keys[:-1]
    if query_values[-1] == ',':
        query_values = query_values[:-1]
    query_keys += ') '
    query_values += ');'
    insert_query += query_keys
    insert_query += query_values
    current_cursor.execute(insert_query)
    db_connection.commit()

def get_item_price(url,browser,price_element_id):
        browser.get(url)
        page_src = browser.page_source
        r = re.compile(price_element_id)
        if r.search(page_src) != None:
            price = r.search(page_src).group(1)
        else:
            price = '-1'  # REWRITE
        return str(price)

def update_db_from_file(browser,file_path,price_element_id,table_name,attrib, constraints):
    with open(file_path, "r", encoding="utf-8") as read_file:
        vaules_list=read_file.readlines()
        for item_url in vaules_list:
            new_price= get_item_price(item_url,browser,price_element_id)
            update_db(table_name, attrib, new_price,constraints)

