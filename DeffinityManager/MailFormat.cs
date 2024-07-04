using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MailFormat
/// </summary>
namespace Deffinity
{
    public class MailFormat
    {
        public MailFormat()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string MailCss(string HeaderTitle)
        {
            string style = @"<html xmlns='http://www.w3.org/1999/xhtml'><head>
                            <title>
                               "+HeaderTitle +@"
                                </title>
                                <meta http-equiv='Content-Type' content='text/html; charset=utf-8' />
                                <style type='text/css'>
                                body{
                                margin:0;
                                font-family:Verdana, Arial, Helvetica, sans-serif;
                                font-size:12px;
                                }
                                table td{
                                padding:5px;
                                }
                                .hdr1{
                                font-size:18px;
                                padding-top:15px;
                                text-align:right;
                                }
                                .hilite{
                                color:#4b0049;
                                }
                                .hdr td{
                                font-size:12px;
                                font-weight:bold;
                                color:#fff;
                                background:#8595a6;
                                text-align:left;
                                }
                                .cont_row td{
                                border:#8595a6 1px solid;
                                color:#219de6;
                                font-weight:bold
                                }
                                .bo_line{
                                border-bottom:#999 10px solid;
                                }
                                .hdrt {
                                font-size:12px;
                                font-weight:bold;
                                color:#fff;
                                height:30px;
                                background:#8595a6;
                                text-align:left;
                                }
                                table.Grid_table {
                                border:#e4dcd3 1px solid;
                                }
                                .Grid_table td{
                                height:15px;
                                padding:5px 0 0 10px;
                                }
                                .odd_row{
                                background:#f2f1f1;
                                color:#636363;
                                }
                                .even_row{
                                background:#fff;
                                color:#636363;
                                }
                                </style> </head>";

            return style;
        }


    }
}
