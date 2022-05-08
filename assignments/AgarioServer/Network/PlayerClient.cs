using System.Net.Sockets;

namespace AgarioServer;

public class PlayerClient
{
    public PlayerState PlayerState;
    public int PlayerServerId;
    public TcpClient PlayerTcpClient;
    public StreamWriter StreamWriter;
}