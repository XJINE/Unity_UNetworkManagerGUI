using UnityEngine;
using XJGUI;

public class UNetworkManagerGUI : MonoBehaviour
{
    #region Field

    public KeyCode toggleIsVisibleKey = KeyCode.N;

    private FlexibleWindow flexibleWindow;
    private IPv4GUI addressGUI;
    private IntGUI  portGUI;
    private BoolGUI autoStartGUI;
    private EnumGUI<UNetworkManager.UNetType> autoStartTypeGUI;

    #endregion Field

    #region Method

    protected virtual void Start()
    {
        this.flexibleWindow = new FlexibleWindow()
        {
            Title     = "UNET",
            IsVisible = false
        };

        this.addressGUI = new IPv4GUI()
        {
            Title = "Address",
            Value = UNetworkManager.singleton.networkAddress,
        };

        this.portGUI = new IntGUI()
        {
            Title      = "Port",
            MinValue   = 0,
            MaxValue   = 65535,
            WithSlider = false,
            Value      = UNetworkManager.singleton.networkPort,
        };

        this.autoStartGUI = new BoolGUI()
        {
            Title = "Auto Start",
            Value = UNetworkManager.singleton.autoStart
        };

        this.autoStartTypeGUI = new EnumGUI<UNetworkManager.UNetType>()
        {
            Title = "Auto Start Type",
            Value = UNetworkManager.singleton.autoStartType
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(this.toggleIsVisibleKey))
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

                addressGUI.Show();
                portGUI.Show();

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

                this.autoStartGUI.Show();
                this.autoStartTypeGUI.Show();

                XJGUILayout.HorizontalLayout(() =>
                {
                    GUILayout.Label("Auto Start After : ");
                    GUILayout.FlexibleSpace();
                    GUILayout.Label(UNetworkManager.singleton.AutoStartIntervalTick.ToString("00.00"));
                });

                UNetworkManager.singleton.networkAddress = addressGUI.Value;
                UNetworkManager.singleton.networkPort    = portGUI.Value;
                UNetworkManager.singleton.autoStart      = this.autoStartGUI.Value;
                UNetworkManager.singleton.autoStartType  = this.autoStartTypeGUI.Value;
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