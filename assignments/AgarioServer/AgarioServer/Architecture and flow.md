## Server Init
**Server:** start and wait for client - OK  
**Server:** Wait for client connection - OK  
**Client:** Connect with player name - OK  
**Server:** CW joined player with name - OK  
**Server:** Send out welcome msg  
**Client:** Present welcome message
**Server:** Send out assigned ID
**Client:** set and log its serverID

# Traffic:
## Server-->Clients
* Orbspawning
* player positions
* player sizes
* player deaths
* player in bounds of level

## Client --> Server
* Updated position
* other playerdata (above)

I guess everything that is cheatable should be calculated server-side. We dont want a second New World situation here...
