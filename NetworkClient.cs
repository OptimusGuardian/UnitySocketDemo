using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;



//Server.cs is in a visual studio project,NetworkClient.cs is attached with MainCamera in a unity project.
namespace SocketDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            /*创建一个socket对象*/
            //寻址方式  套接字类型  协议方式
            Socket tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //绑定监听消息IP和端口号
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            EndPoint endPoint = new IPEndPoint(ip, 6000);
            //向上转型并非是将B自动向上转型为A的对象，相反它是从另一种角度去理解向上两字的：它是对A方法的扩充，即A的对象
            //可访问B从A中集成来的和B复写A的方法。其他的方法都不能访问。即使是A中的私有成员方法。
            tcpSocket.Bind(endPoint);//向操作系统申请一个ip和端口号
            Console.WriteLine("服务器端启动完成");


            //开始监听客户端的连接请求
            //最多可以接受100个客户端请求
            tcpSocket.Listen(100);//backlog 参数指定队列中最多可容纳的等待接受的传入连接数。
            Socket socket = tcpSocket.Accept();//暂停当前线程，直到接受到客户端发来的连接请求；当接收到客户端的连接请求后，
            //在本地创建一个socket与客户端（？？？）连接，并返回出来
            Console.WriteLine("有个客户端连接进来");


            //向客户端发送消息
            string sendMsg;
            sendMsg = "Can i help you?";
            var data = ASCIIEncoding.UTF8.GetBytes(sendMsg);
            socket.Send(data);

            //从客户端接受消息
            byte[] array = new byte[1024];//设置一个消息接受缓冲区
            int getMsg = socket.Receive(array);//该状态处于一个暂停状态，直到接收到消息，并返回字节数组
            Console.WriteLine("接受到从客户端发来的消息：" + ASCIIEncoding.UTF8.GetString(array));
            Console.ReadLine();

        }
    }
}
