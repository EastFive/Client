using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    #region Message_different_type
    /// <summary>
    /// JSON总体消息对象
    /// </summary>
    /// 泛型对象
    public class Pf_Message_Obj<T> where T : new()
    {
        /// <summary>
        /// 消息类型 指令或状态或作业
        /// </summary>
        public string message_type { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public object message_content { get; set; }

        public Pf_Message_Obj(string type, T content)
        {
            message_type = type;
            message_content = content;

        }




    }

    public class Pf_Message_Lane_Object
    {
        public string lane_code { get; set; }
        public string send_time { get; set; }
        public object lane { get; set; }


    }

    public class Pf_Messge_Queue_Object
    {
        public string lane_code { get; set; }
        public string queue_id { get; set; }

        public string action { get; set; }
        public string create_time { get; set; }
        public string send_time { get; set; }
        public object queue { get; set; }

    }

    public class pf_Message_Directive
    {
        public string directive_id { get; set; }
        public string lane_code { get; set; }
        public string lane_name { get; set; }
        public string directive_code { get; set; }
        public string[] parameters { get; set; }
        public string send_time { get; set; }
    }
    #endregion


    #region Lane
    public class Lane
    {
        public string lane_code { get; set; }
        public string lane_name { get; set; }
        public string country_code { get; set; }
        public string city_code { get; set; }
        public string terminal_code { get; set; }
        public string direction { get; set; }
        public bool   has_truck { get; set; }
        public string lane_type { get; set; }
        public string led_display { get; set; }
        public string barrier { get; set; }
        public string update_time { get; set; }
    }
    #endregion
    #region Queue
    public class Queue
    {
        public string lane_code { get; set; }
        public string lane_name { get; set; }
        public string queue_id { get; set; }
        public bool is_lock { get; set; }
        public string lock_time { get; set; }
        public string user { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string truck_no { get; set; }
        public string orc_truck_no { get; set; }
        public string rfid_truck_no { get; set; }
        public string total_weight { get; set; }
        public string ic_card_no { get; set; }
        public int container_amount { get; set; }
        public int damage_check_amount { get; set; }
        public int damage_part_amount { get; set; }
        public int submit_amount { get; set; }
        public string truck_pic_url { get; set; }
        public string truck_crop_pic_url { get; set; }
        public string front_top_pic_url { get; set; }
        public string back_top_pic_url { get; set; }
        public string left_front_pic_url { get; set; }
        public string left_back_pic_url { get; set; }
        public string right_front_pic_url { get; set; }
        public string right_back_pic_url { get; set; }
        public string left_damage_pic_url { get; set; }
        public string right_damage_pic_url { get; set; }
        public string top_damage_pic_url { get; set; }
        public Queue_Progress[] processes { get; set; }
        public Queue_Container[] containers { get; set; }
        public string update_time { get; set; }
    }
    public struct Queue_Progress
    {
        public int step { get; set; }
        public string code { get; set; }
        public string display { get; set; }
        public string status { get; set; }
    }
    public struct Queue_Container
    {
        public string position { get; set; }
        public string container_no { get; set; }
        public string ocr_container_no { get; set; }
        public string iso_code { get; set; }
        public string job_type { get; set; }
        public Queue_Container_Damage damages { get; set; }
        public string send_email { get; set; }
    }
    public struct Queue_Container_Damage
    {
        public string side { get; set; }
        public string damage_code { get; set; }
        public string damaget_grade { get; set; }
        public string remark { get; set; }
    }
    #endregion
    #region Diective
    public class Directive
    {
        public string directive_id { get; set; }

        public string lane_code { get; set; }
        public string lane_name { get; set; }
        public string directive_code { get; set; }
        public string[] parameters { get; set; }
        public string send_time { get; set; }
    }
    #endregion





}
