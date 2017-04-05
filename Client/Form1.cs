using Data.Common;
using Data.Model;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IHubProxy proxy;
        public HubConnection connection;

        public enum messageType
        {
            lane,
            queue,
            devices,

        }

        private Lane lane;

        void SetLane_Value()
        {

            try
            {


                Invoke(new MethodInvoker(() =>
                {
                    text_lane_code.Text = lane.lane_code;
                    text_lane_name.Text = lane.lane_name;
                    text_lane_conutry_code.Text = lane.country_code;
                    text_lane_city_code.Text = lane.city_code;
                    text_lane_terminal_code.Text = lane.terminal_code;
                    text_lane_direction.Text = lane.direction;
                    text_lane_has_truck.Text = lane.has_truck == true ? "True" : "False";
                    text_lane_led_display.Text = lane.led_display;
                    text_lane_barrier.Text = lane.barrier;
                    text_lane_update_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    richTextBox1.AppendText(DateTime.Now + ":" + "接收消息并赋值给控件成功" + "\r\n");
                }));

                if (proxy != null)
                {
                    Pf_Message_Lane_Object lane_obj = new Pf_Message_Lane_Object { lane = this.lane, lane_code = lane.lane_code, send_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                    //Pf_Message_Obj obj = new Pf_Message_Obj { message_type = "lane", message_content = lane_obj };
                    Pf_Message_Obj<Pf_Message_Lane_Object> obj = new Pf_Message_Obj<Pf_Message_Lane_Object>("lane", lane_obj);
                    proxy.Invoke("Change", lane.lane_code, JSONHelper.SerializeObject(obj));//send to messageHub
                    Invoke(new MethodInvoker(() =>
                    {
                        richTextBox1.AppendText(DateTime.Now + ":" + "推送至服务器缓存成功" + "\r\n");
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "赋值给控件失败");
            }

        }

        void GetLane_Value()
        {
            try
            {
                lane.lane_code = text_lane_code.Text;
                lane.lane_name = text_lane_name.Text;
                lane.country_code = text_lane_conutry_code.Text;
                lane.city_code = text_lane_city_code.Text;
                lane.terminal_code = text_lane_terminal_code.Text;
                lane.direction = text_lane_direction.Text;
                lane.has_truck = text_lane_has_truck.Text == "True" ? true : false;
                lane.led_display = text_lane_led_display.Text;
                lane.barrier = text_lane_barrier.Text;
                lane.update_time = text_lane_update_time.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "从控件中获取lane异常");
            }
        }



        private void btn_lane_connect_Click(object sender, EventArgs e)
        {


            Dictionary<string, string> dic = new Dictionary<string, string> { { "Type", "Client" }, { "Name", text_message_lane_code.Text } };
            connection = new HubConnection(text_lane_hub_address.Text, dic);

            proxy = connection.CreateHubProxy(text_lane_hub_name.Text);

            connection.Start().Wait();

            lane = new Lane();
            GetLane_Value();
            Pf_Message_Lane_Object lane_obj = new Pf_Message_Lane_Object { lane = this.lane, lane_code = lane.lane_code, send_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
            Pf_Message_Obj<Pf_Message_Lane_Object> obj = new Pf_Message_Obj<Pf_Message_Lane_Object>("lane", lane_obj);
            proxy.Invoke("Change", lane.lane_code, JSONHelper.SerializeObject(obj));//send to messageHub
            richTextBox1.AppendText(DateTime.Now.ToString() + ":" + "与服务器连接成功" + "\r\n");
            connection.StateChanged += Connection_StateChanged;


            proxy.On("reciveLane", (data) =>
             {
                 Pf_Message_Lane_Object lane_obj_temp = JSONHelper.DeserializeJsonToObject<Pf_Message_Lane_Object>(data);
                 lane = JSONHelper.DeserializeJsonToObject<Lane>(JSONHelper.SerializeObject(lane_obj_temp.lane));
                 SetLane_Value();


             });
        }

        private void Connection_StateChanged(StateChange obj)
        {
            if (obj.NewState == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
            {
                Invoke(new MethodInvoker(() =>
                {
                    richTextBox1.AppendText(DateTime.Now.ToString() + ":" + "与服务器断开连接" + "\r\n");
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (proxy != null)
            {
                GetLane_Value(); //从控件中 获取值
                Pf_Message_Lane_Object lane_obj = new Pf_Message_Lane_Object { lane = this.lane, lane_code = lane.lane_code, send_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                Pf_Message_Obj<Pf_Message_Lane_Object> obj = new Pf_Message_Obj<Pf_Message_Lane_Object>("lane", lane_obj);
                proxy.Invoke("Change", lane.lane_code, JSONHelper.SerializeObject(obj));//send to messageHub
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (proxy != null)
            {
                GetLane_Value();

                Pf_Message_Lane_Object lane_obj = new Pf_Message_Lane_Object { lane = this.lane, lane_code = lane.lane_code, send_time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") };
                Pf_Message_Obj<Pf_Message_Lane_Object> obj = new Pf_Message_Obj<Pf_Message_Lane_Object>("lane", lane_obj);

                string controllane_code = string.Empty;
                Invoke(new MethodInvoker(() =>
                {
                    controllane_code = text_message_lane_code.Text;

                }));
                proxy.Invoke("SendMessage", controllane_code, JSONHelper.SerializeObject(obj));//send to messageHub

            }
        }
    }
}
