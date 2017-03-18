using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MainProjectHos.Models.DataModels
{
    public class EntityAllocaConDocDetails
    {
        public EntityAllocaConDocDetails()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public int TempId { get; set; }
        public int AdmitId { get; set; }

        private int _SrDetailId;

        private System.Nullable<int> _SrNo;

        public string PatientName { get; set; }

        private bool _IsDelete;

        public int SrDetailId
        {
            get
            {
                return this._SrDetailId;
            }
            set
            {
                if ((this._SrDetailId != value))
                {
                    this._SrDetailId = value;
                }
            }
        }

        public System.Nullable<int> SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
                }
            }
        }

        public bool IsDelete
        {
            get
            {
                return this._IsDelete;
            }
            set
            {
                if ((this._IsDelete != value))
                {
                    this._IsDelete = value;
                }
            }
        }

        public int CategoryId { get; set; }
        public int ConsultDocId { get; set; }
        public string CategoryName { get; set; }
        public string ConsultName { get; set; }
        public DateTime Consult_Date { get; set; }
        public decimal ConsultCharges { get; set; }
    }

    public class EntityAllocaConDoc
    {
        public EntityAllocaConDoc()
        {
        }
        private int _SrNo;
        private bool _IsDelete;
        public int SrNo
        {
            get
            {
                return this._SrNo;
            }
            set
            {
                if ((this._SrNo != value))
                {
                    this._SrNo = value;
                }
            }
        }

        public bool IsDelete
        {
            get
            {
                return this._IsDelete;
            }
            set
            {
                if ((this._IsDelete != value))
                {
                    this._IsDelete = value;
                }
            }
        }

        public DateTime Consult_Date { get; set; }
        public int CategoryId { get; set; }
        public int ConsultDocId { get; set; }
        public decimal ConsultCharges { get; set; }
        public string CategoryName { get; set; }
        public string ConsultName { get; set; }

    }
}