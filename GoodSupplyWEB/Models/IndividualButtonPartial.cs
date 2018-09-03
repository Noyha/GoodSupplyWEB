using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GoodSupplyWEB.Models
{
    public class IndividualButtonPartial
    {
        public string ButtonType { get; set; }
        public string Action { get; set; }
        public string Glyph { get; set; }
        public string Text { get; set; }

        public int? MainCategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        //public int? ProductId { get; set; }
        //public string UserId { get; set; }

        public string ActionParameter
        {
            get
            {
                var param = new StringBuilder(@"/");

                if (MainCategoryId != null && MainCategoryId > 0)
                {
                    param.Append(String.Format("{0}", MainCategoryId));
                }
                if (SubCategoryId != null && SubCategoryId > 0)
                {
                    param.Append(String.Format("{0}", SubCategoryId));
                }
                //if (ProductId != null && ProductId > 0)
                //{
                //    param.Append(String.Format("{0}", ProductId));
                //}
                //if (UserId != null && UserId.Trim().Length > 0)
                //{
                //    param.Append(String.Format("{0}", UserId));
                //}

                return param.ToString();
            }
        }
    }
}