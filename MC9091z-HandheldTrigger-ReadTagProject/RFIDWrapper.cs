using System;
using System.Collections.Generic;
using System.Text;
using Symbol.RFID3;
using Symbol.ResourceCoordination;

namespace MC9091z_HandheldTrigger_ReadTagProject
{
    public class RFIDWrapper
    {
        //RFIDReader default values
        private const string hostname = "127.0.0.1";
        private const int port = 5084;
        private const int timeoutMs = 5000;

        private static RFIDWrapper _RFIDWrapper;
        private RFIDReader _reader;
        private TriggerInfo _triggerInfo;

        private Action<string> _callback;

        public static RFIDWrapper Instance
        {
            get
            {
                if (_RFIDWrapper == null)
                {
                    _RFIDWrapper = new RFIDWrapper();
                }
                return _RFIDWrapper;
            }
        }

        /// <summary>
        /// Called by wrapper client code to determine if Reader is present/connected.
        /// In the process, creates a Wrapper object if null, and initializes the reader.
        /// </summary>
        /// <returns></returns>
        public static bool RFIDReaderConnected()
        {
            bool rfidReaderConnected = false;

            try
            {
                if (Instance != null)
                {
                    rfidReaderConnected = true;
                }
            }
            catch (Exception ex)
            {
                //Log("RFID Reader connection failed: {0} ", ex);
                throw;
            }

            return rfidReaderConnected;
        }

        private RFIDWrapper()
        {
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                if (_reader != null)
                {
                    _reader.Events.ReadNotify -= new Events.ReadNotifyHandler(Events_ReadNotify);
                }

                //create RFIDReader using the default values and connect
                _reader = new RFIDReader(hostname, port, timeoutMs);
                _reader.Connect();

                //We are going to read just individual tags, attach tag data with read event
                _reader.Events.AttachTagDataWithReadEvent = true;

                //register read tag event notification
                _reader.Events.ReadNotify += new Events.ReadNotifyHandler(Events_ReadNotify);

                //Set up start and stop triggers to be handheld trigger
                _triggerInfo = new TriggerInfo();

                _triggerInfo.StartTrigger.Type = START_TRIGGER_TYPE.START_TRIGGER_TYPE_HANDHELD;
                _triggerInfo.StartTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_PRESSED;

                _triggerInfo.StopTrigger.Type = STOP_TRIGGER_TYPE.STOP_TRIGGER_TYPE_HANDHELD_WITH_TIMEOUT;
                _triggerInfo.StopTrigger.Handheld.HandheldEvent = HANDHELD_TRIGGER_EVENT_TYPE.HANDHELD_TRIGGER_RELEASED;
                _triggerInfo.StopTrigger.Handheld.Timeout = 0; // 0 = no timeout setting

                /* Setup inventory operation on the reader on the connected antenna, using the handheld trigger.
                 * Inventory starts when the handheld trigger is pressed and stops when released */
                _reader.Actions.Inventory.Perform(null, _triggerInfo, null);

            }
            catch (OperationFailureException ex)
            {
                throw new ApplicationException(String.Format("RFIDReader Initialization Error:{0}", ex.StatusDescription));
            }
        }

        /// <summary>
        /// Read Notify event handler.
        /// Gets the TagID and calls the callback function attached to the wrapper instance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Events_ReadNotify(object sender, Events.ReadEventArgs args)
        {
            try
            {
                if (_callback == null)
                    return;

                TagData tagData = args.ReadEventData.TagData;
                if (tagData != null && tagData.OpStatus == ACCESS_OPERATION_STATUS.ACCESS_SUCCESS)
                {
                    this._callback(tagData.TagID);
                    //Log("Handled RFID Reader Tag Event");
                }

                //purge tags in reader queue
                _reader.Actions.PurgeTags();
            }
            catch (Exception)
            {
                //Log("Error reading tag");
            }
        }


        /// <summary>
        /// Attach callback function to the wrapper object
        /// </summary>
        /// <param name="callback"></param>
        public static void HandleRFIDScan(Action<string> callback)
        {
            Instance._callback = callback;
        }

        /// <summary>
        /// Disconnects reader and disposes. Called by client when finished using the RFID reader.
        /// </summary>
        public static void Disconnect()
        {
            try
            {
                //Log("RFID Reader disconnecting");

                if (Instance != null)
                {
                    Instance._reader.Events.ReadNotify -= new Events.ReadNotifyHandler(Instance.Events_ReadNotify);
                    Instance._triggerInfo = null;
                    Instance._callback = null;
                    Instance._reader.Disconnect();//disposes reader

                    //Log("RFID Reader disconnected");
                }
            }
            catch (Exception ex)
            {
                //Log("Error while disconnecting RFID Reader: {0}", ex);
            }
        }

    }
}
