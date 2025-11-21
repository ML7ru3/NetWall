Description: Write DateTime when done a task.

# 7/11/2025

## Network Capture
- **FEATURE** [x] Make a WinForms that show all the description of the network device selected. ***(07/11/2025)***
    - [x] Know what all the device attributes you can show to the client (ip addr, mac addr, name, ...). 
    - [x] Display to a Winform 
    - [x] Need an icon for network device to display WinForm
- **FEATURE** [x] Capture network packet in selected device. ***(10/11/2025)***
    - [x] Design so that it can start/stop capture packets. 
    - [x] List all packets in a data grid.
- **FEATURE** [x] Make a graph that shows statistics network (like a network speedtest that shows Download/Upload speed (Mbps)) ***(10/11/2025)***
    - [x] Download/Upload statistics.
    - [x] Display to a graph.

## Firewall
- **FEATURE** [x] Firewall integrated. ***(20/11/2025)***

# 10/11/2025
- **FEATURE (CONFIRMING -> DELAYED)** [ ] List all application that using network and their download/upload speed when hover the speed graph. **(VERY HARD)**
    - The idea is: **map packets -> connections -> process IDs (PIDs)**
    - Steps:
        - Capture Packets
        - Get Actice connections + Process IDS
        - Match Packets to Process
    - Ref: https://chatgpt.com/s/t_69140f09441c8191975bf6b142dc4fd1

# 11/11/2025
## Network Capture
- **BUG** [x] Right now, the download speed and upload speed is calculated by average packets in the capturing time. **Expect**: calculated by number of Mb in each second. ***11/11/2025***
- **BUG** [x] When doing internet speed test, the graph does not update =>  **Optimize** ***11/11/2025***
- **FEATURE** [ ] There will be another screen shows all detail list of packets (including data of that packets). Ref: Wireshark.
    - Filter and Search

# 12/11/2025
- **FEATURE** [ ] Save to pcap file after done capturing.

# 17/11/2025
Change of purpose: "personal network monitor" in this context is your network deployed.

-> Solution:
- Deploy an web server that contains all the firewall rules.
- When the app starts, it will connect to that web server to get all the firewall rules.
- Apply those rules to the local machine.

# 21/11/2025

## Purpose now:

Make an web app for the server and minimalism for the app.

Still code in C#.

## Feature 

- [] **FEATURE** Add log to the server so it can track all the activity in that computer
    - The server can see that this computer trying to access to blacklist domain.
- [] **FEATURE** Make an web app for the server for easier inspection
- [] **FEATURE** Make *heartbeat* to the app so that server knows that that computer is still active

# FINAL RELEASE
- [ ] Make an installer for windows.