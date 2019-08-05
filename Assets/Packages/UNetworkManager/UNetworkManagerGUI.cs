using UnityEngine;
using XJGUI;

public class UNetworkManagerGUI : MonoBehaviour
{
    #region Field

    public KeyCode toggleVisibility = KeyCode.N;

    private FlexWindow flexibleWindow;
    private IPv4GUI    ipV4GUI;
    private IntGUI     portGUI;
    private BoolGUI    autoStartGUI;
    private EnumGUI<UNetworkManager.UNetType> autoStartTypeGUI;

    #endregion Field

    #region Method

    protected virtual void Start()
    {
        this.flexibleWindow = new FlexWindow()
        {
            Title     = "UNET",
            IsVisible = false
        };

        this.ipV4GUI      = new IPv4GUI() { Title = "Address" };
        this.portGUI      = new IntGUI()  { Title = "Port", MinValue = 0, MaxValue = 65535, Slider = false };
        this.autoStartGUI = new BoolGUI() { Title = "Auto Start" };
        this.autoStartTypeGUI = new EnumGUI<UNetworkManager.UNetType>() { Title = "Auto Start Type" };
    }

    private void Update()
    {
        if (Input.GetKeyDown(this.toggleVisibility))
        {
            this.flexibleWindow.IsVisible = !this.flexibleWindow.IsVisible;
        }
    }

    private void OnGUI()
    {
        this.flexibleWindow.Show(() => 
        {
            if (UNetworkManager.singleton.NetworkType == UNetworkManager.UNetType.None)
            {
                GUILayout.Label("Connection Settings");

                UNetworkManager.singleton.networkAddress = this.ipV4GUI.Show(UNetworkManager.singleton.networkAddress);
                UNetworkManager.singleton.networkPort    = this.portGUI.Show(UNetworkManager.singleton.networkPort);

                GUILayout.Label("Start");

                XJGUILayout.HorizontalLayout(() =>
                {
                    if (GUILayout.Button("Start as Server"))
                    {
                        UNetworkManager.singleton.StartServerSafe();
                    };

                    if (GUILayout.Button("Start as Host"))
                    {
                        UNetworkManager.singleton.StartHostSafe();
                    };

                    if (GUILayout.Button("Start as Client"))
                    {
                        UNetworkManager.singleton.StartClientSafe();
                    };
                });

                UNetworkManager.singleton.autoStart     = this.autoStartGUI.Show(UNetworkManager.singleton.autoStart);
                UNetworkManager.singleton.autoStartType = this.autoStartTypeGUI.Show(UNetworkManager.singleton.autoStartType);

                XJGUILayout.HorizontalLayout(() =>
                {
                    GUILayout.Label("Auto Start After : ");
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(UNetworkManager.singleton.AutoStartIntervalTick.ToString("00.00"));
                });
            }

            else
            {
                GUILayout.Label("Connection Type");
                GUILayout.Label(UNetworkManager.singleton.NetworkType.ToString());

                GUILayout.Label("Connection Info");
                GUILayout.Label("Address : " + UNetworkManager.singleton.networkAddress);
                GUILayout.Label("Port : "    + UNetworkManager.singleton.networkPort);

                if (GUILayout.Button("Stop Connection"))
                {
                    UNetworkManager.singleton.Stop();
                }
            }

            GUILayout.Label("Status");

            foreach (UNetworkManager.StatusMessage statusMessage
                    in UNetworkManager.singleton.StatusMessages)
            {
                GUILayout.Label(statusMessage.time.ToLongTimeString() + " - " + statusMessage.message);
            }
        });
    }

    #endregion Method
}