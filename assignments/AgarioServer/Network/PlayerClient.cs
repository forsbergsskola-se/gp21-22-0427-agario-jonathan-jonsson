using System.Net.Sockets;
using AgarioServer.Model;

namespace AgarioServer.Network;

public class PlayerClient
{
    public PlayerState PlayerState;
    public int PlayerServerId;
    public TcpClient PlayerTcpClient;
    public StreamWriter StreamWriter;
}