import json
import hashlib
import os



def write_to_file(data, file_path, data_format, file_format):

    with open(file_path, 'w', encoding="utf-8") as file:
        if file_format.lower() == 'txt':
            delimetr = ''
            if data_format.lower() == 'csv':
                delimetr = ','
            if type(data) is list:
                for data_item in data:
                    if type(data_item) is str:
                        file.write(data_item+'\n')
                    else:
                        # REWRITE!
                        tmp_str = ''
                        for item in data_item:
                            tmp_str += item+delimetr
                        tmp_str = tmp_str[0:len(tmp_str)-1]
                        tmp_str += '\n'
                        file.write(tmp_str)
                    file.close()
        if file_format.lower() == 'html':

            file.write(data)
            file.close()


def read_from_file(file_path, data_format, file_format):

    with open(file_path, 'r', encoding="utf-8") as file:
        if file_format.lower() == 'txt':
            delimetr = ''
            if data_format.lower() == 'csv':
                delimetr = ','
                file_data = file.readlines()
                data_list = [x.replace('\n', '').split(delimetr)
                             for x in file_data]
                file.close()
                return data_list

        if file_format.lower() == 'html':
            data = file.readlines()
            file.close()
            return data


def parse_json_object(json_path, url):
    with open(json_path, "r", encoding="utf-8") as read_file:
        data = json.load(read_file)
        for item in data:
            if item["site_start_url"] == url:
                read_file.close()
                return item
    read_file.close()
    return None


def getItemHash(item_val):
    return hashlib.md5(item_val.encode('utf-8'))


