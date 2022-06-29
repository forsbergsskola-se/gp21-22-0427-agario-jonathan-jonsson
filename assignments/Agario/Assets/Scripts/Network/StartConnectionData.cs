using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Network
{
    public class StartConnectionData : MonoBehaviour //TODO: The whole move an object as data holder between main menu and main scene feels ugly, since it is never used again... fix!
    {
        public TMP_InputField nameInput;
        public TcpClient TcpClient = new TcpClient();
        public string playerName;
        public void ConnectOnClick() => Connect();

        private async Task Connect()
        {
            await TcpClient.ConnectAsync(IPAddress.Loopback, 1313);
            playerName = nameInput.text;
            SceneManager.LoadSceneAsync("AgarioMain");
        }

        private void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
