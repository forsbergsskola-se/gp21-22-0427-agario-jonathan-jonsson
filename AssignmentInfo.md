Implemented in Agario Game:  

# Server Init and game start
* Server
  * Listening and accepting Connections from player clients
  * When a player connects, it sends a server-unique ID to the player client and welcome message
  * Server sends default data (Speed, score, size and a random start position)
  * Server starts a new Task to listen to incoming messages. (OBS! happens once and is not relating to player connecting)
 
* Client
  * The player enters their name on start screen and connects.
  * Connects to the server and passing TcpClient to server, sending name.
  * Starts a new Task to read incoming messages
  * Receives ID +  default data from server and player is spawned into the world, keeping name from initial screen.

# During the game
* Server
  * Send a spawn orb message at predetermined interval to client, containing orbID and a random position on the game board.
  * Reads player position message and evaluates it so that it is not an illegal position (e.g. outside game board). If it is, the server sends a correction to the client.

* Client
  * Player move with WASD and have a camera following the player
  * Broadcasts its position and sends it to the server
  * If the player exits the game board the client is being corrected by the server
  * Sends a message to the server when colliding with an orb for validation, based on orbID  and the players position. The distance between player and orb (taking radius of player into account) is evaluated and checked to be below a tolerance. If it is, the client receives confirmation and the orb is eaten.
  * Spawns orb after receiving instructions from server to do so


# Next steps
* Refactor the whole thing. A lot to fix
  * Implementing reflection for the messaging system
  * Use a common message handler between server and client
* Name is currently transfered inbetween scenes in unity by a GO. Should be sent with default package from server.
* Implement missing game features (a lot)
