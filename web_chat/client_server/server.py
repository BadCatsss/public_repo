import socket
import time
import re
import json
import errno
from random import randrange
import threading
udp_data=None
tcp_data=None
tcp_conn=None
udp_client_addr=''
tcp_cliet_addr=''
server_private_ip=''
server_public_ip=''
max_tcp_connections=10
msg_history=list()
server_was_stopped=False
msg_history_length=100
active_clients={}
active_clients_niknames=set()
default_port_val=49001
udp_socket_obj=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
tcp_socket_obj=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
server_host=''
server_port=''
tcp_buffer_size=1024
udp_buffer_size=1024
MIN_PORT_RANGE=1
MAX_PORT_RANGE=65535
ALTERNATIVE_MIN_PORT_RANGE=48654
ALTERNATIVE_MAX_PORT_RANGE=48999
recv_udp_msg_thread=None
recv_tcp_msg_thread=None

def read_config():
    global msg_history_length
    try:
        with open("settings.json", "r") as config_file:
            config_val = json.load(config_file)
        msg_history_length=config_val["msg_history_length"]
    except:
     with open("settings.json", "w") as config_file:
       config_file.write("{\"msg_history_length\":"+str(msg_history_length)+"}")

def get_gray_ip():
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    try:
        # doesn't even have to be reachable
        s.connect(('10.255.255.255', 1))
        IP = s.getsockname()[0]
    except Exception:
        IP = '127.0.0.1'
    finally:
        s.close()
    return IP

def init_server():
    global server_private_ip
    global server_public_ip
    global server_port
    global recv_tcp_msg_thread
    global recv_udp_msg_thread

    server_private_ip=get_gray_ip()
    server_public_ip=socket.gethostbyname(socket.gethostname())
    print("Private ip:"+str(server_private_ip))
    print("Public ip:"+str(server_public_ip))
    print("What ip use:public  - "+str(server_public_ip)+" (WAN) or private - "+str(server_private_ip)+" (LAN)\n"
    +"1-LAN\n 2-WAN")
    server_mode=''
    print("Enter mode number (1 or 2)")
    ip_addr_mode=input()
    while ip_addr_mode!='1' and ip_addr_mode!='2':
        print("Enter mode number (1 or 2)")
        ip_addr_mode=input()
    if ip_addr_mode=='1':
        server_host=server_private_ip
        server_mode='LAN'
    else:
        server_host=server_public_ip
        server_mode='WAN'
    print("input port number for listening:\n")
    server_port=input()
    if not server_port.isnumeric() or int(server_port)<MIN_PORT_RANGE or int(server_port)>MAX_PORT_RANGE:
        server_port=default_port_val
    server_port=int(server_port)
    try_connect(3)
    print("selected server port:"+str(server_port))
    print("Active server address:"+server_mode+" - "+str(server_host))
    print("Server public ip:"+server_public_ip)
    print("Server private ip:"+server_private_ip)
    read_config()
    recv_udp_msg_thread=threading.Thread(target=recive_udp,args=(udp_buffer_size,))
    recv_udp_msg_thread.start()
    recv_tcp_msg_thread=threading.Thread(target=recive_tcp,args=(tcp_buffer_size,))
    recv_tcp_msg_thread.start()
    print('Server started')


def try_connect(try_count):
    global server_host
    global server_port
    global udp_socket_obj
    global tcp_socket_obj
    if not hasattr(try_connect,"try_connect_count"):
        try_connect.try_connect_count=0
    try: #todo rewrite
        #udp
        udp_socket_obj.bind((server_host,server_port))
        #tcp
        tcp_socket_obj.bind((server_host,server_port))
        tcp_socket_obj.listen(max_tcp_connections)
        
    except socket.error as err:
        if err.errno==errno.EADDRINUSE:
            server_host=socket.gethostbyname(socket.gethostname())
            server_port=randrange(ALTERNATIVE_MIN_PORT_RANGE,ALTERNATIVE_MAX_PORT_RANGE)
            if try_connect.try_connect_count<try_count:
                try_connect.try_connect_count
                try_connect(try_count)
            else:
                try_connect.try_connect_count=0



def search_nikname(data):
    global active_clients_niknames
    if re.search('.*?(\[.*\]).*', data)!=None:
                active_clients_niknames.add(re.search('.*?(\[.*\]).*', data).group(1))

def send_udp(udp_socket_obj,msg,client):
    udp_socket_obj.sendto(msg.encode("utf-8"),client)

def send_tcp(tcp_socket_obj,msg,client=""):
    tcp_socket_obj.send(msg)


def check_active_clients(socket_obj):
    global active_clients
    global msg_history
    global active_clients_niknames
    global msg_history_length
    while not server_was_stopped:
        try:
            if socket_obj.type==socket.SOCK_DGRAM:
                send_method=send_udp
                addr=udp_client_addr
                data=udp_data
            elif socket_obj.type==socket.SOCK_STREAM:
                send_method=send_tcp
                addr=tcp_cliet_addr
                data=tcp_data
            
            if addr!='' and addr[1] not in active_clients.keys():
                    active_clients[addr[1]]=addr[0]
                    if len(msg_history)!=0:
                        for msg in msg_history:
                            send_method(socket_obj,str(msg),(active_clients[addr[1]],addr[1]))
                        search_nikname(data)
                    connected_time= time.strftime("%Y-%m-%d-%H.%M.%S",time.localtime())
                    print("["+str(addr[0])+"]=["+str(addr[1])+"]=["+str(connected_time)+"]/",end="")
                    if data!=None and data!='':
                        print(str(data))
                        if len(msg_history)>msg_history_length:
                            msg_history.clear()
                        msg_history.append(str(data))

                    if re.search('^.*\:\: (!userlist)$', str(data))!=None: #command
                        for chat_user in active_clients_niknames:
                            send_method(socket_obj,str(chat_user),(active_clients[addr[1]],addr[1]))
                    else:
                        for client_id in active_clients:
                                    if addr[1]!=client_id:
                                        send_method(socket_obj,data,(active_clients[client_id],client_id))
        except socket.error as e:
            if e.winerror==10054:
                active_clients.pop(addr)



def recive_udp(packet_size):
    global udp_data
    global udp_client_addr
    global server_was_stopped
    while not server_was_stopped:
        try:
            udp_data,udp_client_addr=udp_socket_obj.recvfrom(packet_size)
        except Exception as e:
            print("\n[Server stopped]")
            print(str(e))
            server_was_stopped=True
            udp_socket_obj.close()
    

def recive_tcp(frame_size):
    global tcp_data
    global tcp_conn
    global tcp_cliet_addr
    global server_was_stopped
    while not server_was_stopped:
        tcp_conn,tcp_cliet_addr=tcp_socket_obj.accept()
        try:
           tcp_data=tcp_conn.recv(frame_size)
        except Exception as e:
            print("\n[Server stopped]")
            print(str(e))
            server_was_stopped=True
            udp_socket_obj.close()
    
    
init_server()
# check_tcp_clients_thread=threading.Thread(target=check_active_clients,args=(tcp_cliet_addr,tcp_data,tcp_socket_obj))
# check_tcp_clients_thread.start()
# check_udp_clients_thread=threading.Thread(target=check_active_clients,args=(udp_client_addr,str(udp_data),udp_socket_obj))
# check_udp_clients_thread.start()
while not server_was_stopped:
    try:
        connected_time=""
        check_active_clients(tcp_socket_obj)
        check_active_clients(udp_socket_obj)
    except Exception as e:
        print("\n[Server stopped]")
        print(str(e))
        server_was_stopped=True
udp_socket_obj.close()