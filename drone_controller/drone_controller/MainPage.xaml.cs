using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Bluetooth;
using Android.Bluetooth.LE;

namespace drone_controller
{
    public partial class MainPage : ContentPage
    {
        BluetoothLeAdvertiser _mBleAd;
        MyAdvertiseCallback _myAdvertiseCallback = new MyAdvertiseCallback();
        public MainPage()
        {
            InitializeComponent();
            InitBleAd();
        }   
        
        private void InitBleAd()
        {
            BluetoothManager mBluetoothManager = (BluetoothManager)Android.App.Application.Context.GetSystemService(Android.Content.ContextWrapper.BluetoothService);
            BluetoothAdapter mBluetoothAdapter = mBluetoothManager.Adapter;
            BluetoothLeAdvertiser mBluetoothLeAdvertiser = mBluetoothAdapter.BluetoothLeAdvertiser;
            _mBleAd = mBluetoothLeAdvertiser;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                AdvertiseSettings settings = new AdvertiseSettings.Builder()
                .SetAdvertiseMode(AdvertiseMode.Balanced)
                .SetConnectable(false)
                .SetTimeout(0)
                .SetTxPowerLevel(AdvertiseTx.PowerMedium)
                .Build();

                //string guid = Guid.NewGuid().ToString();
                
                //Java.Util.UUID javaUuid = Java.Util.UUID.FromString(guid);
                //ParcelUuid parelUuid = new ParcelUuid(javaUuid);

                //String strPayload = "hedbun";
                //byte[] payload = Encoding.UTF8.GetBytes(strPayload);
                
                AdvertiseData data = new AdvertiseData.Builder()
                    .SetIncludeDeviceName(true)
                    //.AddServiceUuid(parelUuid)
                    //.AddServiceData(uuid, payload)
                    .SetIncludeTxPowerLevel(false)                    
                    .Build();

                _mBleAd.StartAdvertising(settings, data, _myAdvertiseCallback);
                //bluetoothManager.Adapter.BluetoothLeAdvertiser.StartAdvertising(settings, data, myAdvertiseCallback);
            }
            else
            {
                _mBleAd.StopAdvertising(_myAdvertiseCallback);
                //bluetoothManager.Adapter.BluetoothLeAdvertiser.StopAdvertising(myAdvertiseCallback);
            }                                    
        }
    }

    public class MyAdvertiseCallback : AdvertiseCallback
    {
        public override void OnStartFailure([GeneratedEnum] AdvertiseFailure errorCode)
        {
            base.OnStartFailure(errorCode);
        }

        public override void OnStartSuccess(AdvertiseSettings settingsInEffect)
        {
            base.OnStartSuccess(settingsInEffect);
        }
    }

    //private static ParcelUuid parseUuidFrom(byte[] uuidBytes)
    //{
    //    /** Length of bytes for 16 bit UUID */
    //    const int UUID_BYTES_16_BIT = 2;
    //    /** Length of bytes for 32 bit UUID */
    //    const int UUID_BYTES_32_BIT = 4;
    //    /** Length of bytes for 128 bit UUID */
    //    const int UUID_BYTES_128_BIT = 16;
    //    ParcelUuid BASE_UUID =
    //            ParcelUuid.FromString("00000000-0000-1000-8000-00805F9B34FB");
    //    if (uuidBytes == null)
    //    {
    //        //throw new IllegalArgumentException("uuidBytes cannot be null");
    //    }
    //    int length = uuidBytes.Length;
    //    if (length != UUID_BYTES_16_BIT && length != UUID_BYTES_32_BIT &&
    //            length != UUID_BYTES_128_BIT)
    //    {
    //        //throw new IllegalArgumentException("uuidBytes length invalid - " + length);
    //    }
    //    // Construct a 128 bit UUID.
    //    if (length == UUID_BYTES_128_BIT)
    //    {
    //        MemoryStream ms = new MemoryStream(uuidBytes);
    //        ms.or
    //        ByteBuffer buf = ByteBuffer.wrap(uuidBytes).order(ByteOrder.LITTLE_ENDIAN);
    //        long msb = buf.getLong(8);
    //        long lsb = buf.getLong(0);
    //        return new ParcelUuid(new UUID(msb, lsb));
    //    }
    //    // For 16 bit and 32 bit UUID we need to convert them to 128 bit value.
    //    // 128_bit_value = uuid * 2^96 + BASE_UUID
    //    long shortUuid;
    //    if (length == UUID_BYTES_16_BIT)
    //    {
    //        shortUuid = uuidBytes[0] & 0xFF;
    //        shortUuid += (uuidBytes[1] & 0xFF) << 8;
    //    }
    //    else
    //    {
    //        shortUuid = uuidBytes[0] & 0xFF;
    //        shortUuid += (uuidBytes[1] & 0xFF) << 8;
    //        shortUuid += (uuidBytes[2] & 0xFF) << 16;
    //        shortUuid += (uuidBytes[3] & 0xFF) << 24;
    //    }
    //    long msb = BASE_UUID.getUuid().getMostSignificantBits() + (shortUuid << 32);
    //    long lsb = BASE_UUID.getUuid().getLeastSignificantBits();
    //    return new ParcelUuid(new UUID(msb, lsb));
    //}
}
