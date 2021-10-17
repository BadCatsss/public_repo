import socket,time,threading,re
import errno
import atexit
from enum import Enum


class Protocols(Enum):
    UDP=1
    TCP=2
server_addr=None
server_port=None
server_default_port_val=49001
client_Is_shutdown=False
client_was_join=False
recv_msg_thread=None
message_protocol=None
called_method_for_send=None
client_socket_obj=None
snd_data=None
server_inst=None

exit_msg='end_session'
def client_logoff():
    called_method_for_send((exit_msg).encode("utf-8"),snd_data)
atexit.register(client_logoff)

def config_connection():
    global message_protocol
    global server_addr
    global server_port
    global client_socket_obj
    global called_method_for_send
    global recv_msg_thread
    global server_inst
    global snd_data

    print("Enter server ip and server port:")
    print("server ip:\n")
    server_addr=input()
    print("server port:\n")
    server_port=input()
    if re.fullmatch(r'^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$',server_addr) is None:
        server_addr=socket.gethostbyname(socket.gethostname())
    if not server_port.isnumeric() or int(server_port)>65535 or int(server_port)<0:
        server_port=server_default_port_val
    print("selected server ip:"+str(server_addr))
    print("selected server port:"+str(server_port))
    server_port=int(server_port)
    print("select message protocol:UDP - "+str(Protocols.UDP.value)+";TCP-"+str(Protocols.TCP.value))
    message_protocol=input()
    while   str(message_protocol)!=str(Protocols.UDP.value) and  message_protocol!=str(Protocols.TCP.value):
        print("select message protocol:UDP - "+str(Protocols.UDP.value)+";TCP-"+str(Protocols.TCP.value))
        message_protocol=input()
    if int( message_protocol)==Protocols.UDP.value:
        client_socket_obj=socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
        recv_msg_thread=threading.Thread(target=reciving_msg,args=("RecvThread",client_socket_obj,client_socket_obj.recvfrom,1024))
        called_method_for_send=client_socket_obj.sendto
        server_inst=(server_addr,server_port)
        snd_data=server_inst
        client_socket_obj.bind(('',server_port))
    elif int( message_protocol)==Protocols.TCP.value:
        client_socket_obj=socket.socket(socket.AF_INET,socket.SOCK_STREAM)
        server_inst=(server_addr,server_port)
        snd_data=0
        recv_msg_thread=threading.Thread(target=reciving_msg,args=("RecvThread",client_socket_obj,client_socket_obj.recv,None))
        called_method_for_send=client_socket_obj.send
        client_socket_obj.connect(server_inst)
        

def reciving_msg(name,sock,recv_method,recv_pckg_size):
    global client_Is_shutdown

    while not client_Is_shutdown:
        try:
            while True:
                if recv_pckg_size!=None:
                    data,addr=sock.recv_method(recv_pckg_size)
                else:
                    data,addr=sock.recv_method()
                print(data.decode("utf-8"))
                time.sleep(0.2)
        except:
            pass
config_connection()

client_host=socket.gethostbyname(socket.gethostname())
# client_port=0
# client_socket_obj.bind((client_host,client_port))
alias=input("Name:")


recv_msg_thread.start()

while client_Is_shutdown==False:
    if client_was_join==False:
        called_method_for_send(("["+alias+"]=>join chat").encode("utf-8"),snd_data)
        client_was_join=True
    else:
        try:
            message=input()
            if message!="":
                called_method_for_send(("["+alias+"] :: "+message).encode("utf-8"),snd_data)
            time.sleep(0.2)
        except socket.error as err:
            if err.winerror!=10054:
             called_method_for_send(("["+alias+"] <= left chat").encode("utf-8"),snd_data)
             client_Is_shutdown=True
recv_msg_thread.join()
client_socket_obj.close()