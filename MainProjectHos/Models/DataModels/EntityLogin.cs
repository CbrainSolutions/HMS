using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace MainProjectHos.Models.DataModels
{
    /// <summary>
    /// Summary description for EntityLogin
    /// </summary>
    public class EntityLogin
    {
        public EntityLogin()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private int _PKId;

        private string _UserName;

        private string _Password;

        private string _UserType;

        private bool _Discontinued;

        private bool _IsFirstLogin;

        public int PKId
        {
            get
            {
                return this._PKId;
            }
            set
            {
                if ((this._PKId != value))
                {
                    this._PKId = value;
                }
            }
        }

        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                if ((this._UserName != value))
                {
                    this._UserName = value;
                }
            }
        }

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                if ((this._Password != value))
                {
                    this._Password = value;
                }
            }
        }

        public string UserType
        {
            get
            {
                return this._UserType;
            }
            set
            {
                if ((this._UserType != value))
                {
                    this._UserType = value;
                }
            }
        }

        public bool Discontinued
        {
            get
            {
                return this._Discontinued;
            }
            set
            {
                if ((this._Discontinued != value))
                {
                    this._Discontinued = value;
                }
            }
        }

        public bool IsFirstLogin
        {
            get
            {
                return this._IsFirstLogin;
            }
            set
            {
                if ((this._IsFirstLogin != value))
                {
                    this._IsFirstLogin = value;
                }
            }
        }

        public string OldPass { get; set; }

        public object ConfirmPass { get; set; }

        public string NewPass { get; set; }
    }
}