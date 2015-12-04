using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace Zkly.Common.SMS
{
    public class SmsResult
    {
        private static readonly IDictionary<string, string> Map = new Dictionary<string, string>
        {
            { "0", "�ɹ�" },
            { "500", "�����쳣" },
            { "-1", "������ʽ����" },
            { "-2", "ע��Ż����������" }, 
            { "-3", "�����������δ���" }, 
            { "-4", "����Ա���ƻ��������" }, 
            { "-5", "Unknown Error" }, 
            { "-6", "û������ע���" }, 
            { "-7", "û����������" }, 
            { "-8", "û�������ֻ���" }, 
            { "-9", "û�������������" }, 
            { "-10", "�������ݳ���������560���ַ�" }, 
            { "-11", "����" }, 
            { "-12", "�����������зǷ���" }, 
            { "-13", "���Ű��е��ֻ�������������1000��" }, 
            { "-14", "��ʱʱ�䲻��Ϊ��" }, 
            { "-15", "�Ǳ�ƽ̨��ע���" }, 
            { "-16", "ע��Ų�����" }, 
            { "-17", "��ֵ������Ϊ����" }, 
            { "-18", "�۷ѽ�����Ϊ����" }, 
            { "-19", "�̹߳��࣬����4" }
        };

        private readonly NameValueCollection _data;

        internal SmsResult(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                throw new ArgumentNullException("queryString");
            }

            _data = HttpUtility.ParseQueryString(queryString);
        }

        public bool Success
        {
            get { return Code == "0"; }
        }

        public string ErrorMessage
        {
            get
            {
                if (Success)
                {
                    return null;
                }

                string msg;
                return Map.TryGetValue(Code, out msg) ? msg : "δ֪����";
            }
        }

        public string Code
        {
            get { return _data["result"]; }
        }

        public string Message
        {
            get { return _data["message"] ?? "�ɹ�"; }
        }

        /// <summary>
        /// ����ʣ��������
        /// </summary>
        public string Balance
        {
            get { return _data["balance"]; }
        }

        /// <summary>
        /// �����ύ����
        /// </summary>
        public string DailySubmited
        {
            get { return _data["total"]; }
        }

        /// <summary>
        /// �ȴ���������
        /// </summary>
        public string WaitNumber
        {
            get { return _data["waitnum"]; }
        }

        /// <summary>
        /// ���ڷ�������
        /// </summary>
        public string SendingNumber
        {
            get { return _data["sendingnum"]; }
        }

        /// <summary>
        /// �ɹ�����
        /// </summary>
        public string SuccessNumber
        {
            get { return _data["sucessnum"]; }
        }

        /// <summary>
        /// ʧ������
        /// </summary>
        public string FailNumber
        {
            get { return _data["failnum"]; }
        }

        /// <summary>
        /// ���Ű�ID
        /// </summary>
        public string SmsId
        {
            get { return _data["smsid"]; }
        }
    }
}