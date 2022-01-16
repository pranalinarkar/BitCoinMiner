# AkkaCoinMiner
Coin Miner Using Akka.FSharp API

## How to import the project?

Install following tools
1. .Net 5.0 SDK (https://dotnet.microsoft.com/download)
2. Visual Studio Project IDE (Community Edition 2019) (https://visualstudio.microsoft.com/vs/)

Steps to import the project:
1. Open Visual Studio 2019 and select **Open a project or solution**

![alt text](https://i.ibb.co/Bf1FWMh/1.png)


2. Navigate to the directory where you have cloned the repo and select the **CoinMiner.fsproj** file.

![alt text](https://i.ibb.co/ph6Jsvh/2.png)


Steps to run the program

1. There are two types of mode in which our AKKA App will run
    * Single Node - App will run on only one machine where all the actors will reside.
    * Cluster - App will run in distributed mode where actors will be distributed across nodes who participate in the cluster
  
2. To run the console application from visual studio, click on the Runtime Configuration dropdown and select the **Startup Project Properties** option.

![alt text](https://i.ibb.co/pKknp83/3.png)

3. To run the application in Single Node mode, add following line to the Application arguments and save it (Ctrl + S)

singleNode 5

![alt text](https://i.ibb.co/hYLXmGm/4.png)

4. After this press Ctrl + F5. This will start your application.

![alt text](https://i.ibb.co/8xpjktf/5.png)

5. To run the application in cluster mode, add following line to the Application arguments and save it (Ctrl + S) and then run the application (Ctrl + F5)

seed 4 127.0.0.1 9000

![alt text](https://i.ibb.co/YLKfmPH/6.png)

This will start your seed node which is your master server node to which other client nodes will connect.

![alt text](https://i.ibb.co/X4jygPd/7.png)

6. To start a client node, add following line to the Application arguments and save it (Ctrl + S ) and the run the application (Ctrl + F5).

client 4 127.0.0.1 9000 127.0.0.1

![alt text](https://i.ibb.co/k1FbBJv/10.png)

This will start the client node which will connect to the seed node and start mining the coins.

![alt text](https://i.ibb.co/Y0PB4rH/9.png)
