class html_node_obj:
    def __init__(self, start_search_url, node_container, find_href_text, find_url_class, find_children_list, parent_node_obj):
        self.start_search_url = start_search_url
        self.node_container = node_container
        self.find_href_text = find_href_text
        self.find_url_class = find_url_class
        self.find_children_list = find_children_list
        self.parent_node_obj = parent_node_obj
        self.category_name = ""