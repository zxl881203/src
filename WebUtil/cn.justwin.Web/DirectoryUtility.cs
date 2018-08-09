namespace cn.justwin.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    /// <summary>
    /// 文件夹辅助类
    /// </summary>
    public class DirectoryUtility
    {
        private DirectoryInfo dirInfo;
        public string path;

        public DirectoryUtility(string path)
        {
            this.path = path;
        }

        /// <summary>
        /// 返回所有附件
        /// </summary>
        /// <returns></returns>
        public List<Annex> GetAnnex()
        {
            List<Annex> list = new List<Annex>();
            try
            {
                DirectoryInfo info = new DirectoryInfo(this.absolutePath);
                foreach (FileInfo info2 in info.GetFiles())
                {
                    Annex item = new Annex {
                        Name = info2.Name,
                        Length = Math.Round((double) (((double) info2.Length) / 1024.0), 2, MidpointRounding.AwayFromZero) + "kb",
                        Path = this.path
                    };
                    list.Add(item);
                }
            }
            catch
            {
            }
            return list;
        }

        /// <summary>
        /// 返回所有附件
        /// </summary>
        /// <param name="readOnly">是否只读</param>
        /// <returns></returns>
        public List<Annex> GetAnnex(bool readOnly)
        {
            List<Annex> list = new List<Annex>();
            try
            {
                DirectoryInfo info = new DirectoryInfo(this.absolutePath);
                foreach (FileInfo info2 in info.GetFiles())
                {
                    Annex item = new Annex {
                        Name = info2.Name,
                        Length = Math.Round((double) (((double) info2.Length) / 1024.0), 2, MidpointRounding.AwayFromZero) + "kb",
                        ReadOnly = readOnly,
                        Path = this.path
                    };
                    list.Add(item);
                }
            }
            catch
            {
            }
            return list;
        }

        /// <summary>
        /// 返回所有附件
        /// </summary>
        /// <returns></returns>
        public  List<Annex> GetAnnex(string text2)
        {
            List<Annex> list = new List<Annex>();
            try
            {
                DirectoryInfo info = new DirectoryInfo(this.absolutePath);
                foreach (FileInfo info2 in info.GetFiles())
                {
                    Annex item = new Annex
                    {
                        Name = System.Web.HttpUtility.UrlDecode(info2.Name, System.Text.Encoding.GetEncoding("GB2312")),//MyUrlDeCode(info2.Name, Encoding.UTF8),
                        Length = Math.Round((double)(((double)info2.Length) / 1024.0), 2, MidpointRounding.AwayFromZero) + "kb",
                        Path = text2
                    };
                    list.Add(item);
                }
            }
            catch
            {
            }
            return list;
        }

        /// <summary>
        /// 返回所有附件
        /// </summary>
        /// <param name="readOnly">是否只读</param>
        /// <returns></returns>
        public  List<Annex> GetAnnex(bool readOnly, string text2)
        {
            List<Annex> list = new List<Annex>();
            try
            {
                DirectoryInfo info = new DirectoryInfo(this.absolutePath);
                foreach (FileInfo info2 in info.GetFiles())
                {
                    Annex item = new Annex
                    {
                        Name = MyUrlDeCode(info2.Name, Encoding.UTF8),
                        Length = Math.Round((double)(((double)info2.Length) / 1024.0), 2, MidpointRounding.AwayFromZero) + "kb",
                        ReadOnly = readOnly,
                        Path = text2
                    };
                    list.Add(item);
                }
            }
            catch
            {
            }
            return list;
        }
        ///// <summary>
        ///// 返回所有附件
        ///// </summary>
        ///// <returns></returns>
        //public List<Annex2> GetAnnex(string text2, string text)
        //{
        //    List<Annex2> list = new List<Annex2>();
        //    try
        //    {
        //        DirectoryInfo info = new DirectoryInfo(this.absolutePath);
        //        foreach (FileInfo info2 in info.GetFiles())
        //        {
        //            Annex item = new Annex
        //            {
        //                Name = System.Web.HttpUtility.UrlDecode(info2.Name, System.Text.Encoding.GetEncoding("GB2312")),//MyUrlDeCode(info2.Name, Encoding.UTF8),
        //                Length = Math.Round((double)(((double)info2.Length) / 1024.0), 2, MidpointRounding.AwayFromZero) + "kb",
        //                Path = text2,
        //                //ServerId =WXAPI.uploadVoice(filepath)
        //            };
        //            list.Add(item);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 返回所有附件
        ///// </summary>
        ///// <param name="readOnly">是否只读</param>
        ///// <returns></returns>
        //public List<Annex> GetAnnex(bool readOnly, string text2, string text)
        //{
        //    List<Annex> list = new List<Annex>();
        //    try
        //    {
        //        DirectoryInfo info = new DirectoryInfo(this.absolutePath);
        //        foreach (FileInfo info2 in info.GetFiles())
        //        {
        //            Annex item = new Annex
        //            {
        //                Name = MyUrlDeCode(info2.Name, Encoding.UTF8),
        //                Length = Math.Round((double)(((double)info2.Length) / 1024.0), 2, MidpointRounding.AwayFromZero) + "kb",
        //                ReadOnly = readOnly,
        //                Path = text2
        //            };
        //            list.Add(item);
        //        }
        //    }
        //    catch
        //    {
        //    }
        //    return list;
        //}

        public static string MyUrlDeCode(string str, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding utf8 = Encoding.UTF8;
                //首先用utf-8进行解码                    
                string code = HttpUtility.UrlDecode(str.ToUpper(), utf8);
                //将已经解码的字符再次进行编码.
                string encode = HttpUtility.UrlEncode(code, utf8).ToUpper();
                if (str == encode)
                    encoding = Encoding.UTF8;
                else
                    encoding = Encoding.GetEncoding("gb2312");
            }
            return HttpUtility.UrlDecode(str, encoding);
        }







        /// <summary>
        /// 返回当前路径下所有的文件名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetAnnexName()
        {
            List<string> list = new List<string>();
            if (!string.IsNullOrWhiteSpace(this.path))
            {
                try
                {
                    DirectoryInfo info = new DirectoryInfo(this.absolutePath);
                    list = (from f in info.GetFiles() select f.Name).ToList<string>();
                }
                catch
                {
                }
            }
            return list;
        }

        /// <summary>
        /// 返回当前路径下的所有文件名称
        /// 文件名称之间用逗号"，"隔开
        /// </summary>
        /// <returns></returns>
        public string GetAnnexNames()
        {
            List<string> annexName = this.GetAnnexName();
            return string.Join(",", annexName.ToArray());
        }

        /// <summary>
        /// path的绝对路径
        /// </summary>
        public string absolutePath
        {
            get
            {
                try
                {
                    return HttpContext.Current.Server.MapPath(this.path);
                }
                catch
                {
                    return this.path;
                }
            }
        }

        public DirectoryInfo DirInfo
        {
            get
            {
                return this.dirInfo;
            }
            set
            {
                this.dirInfo = value;
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = value;
            }
        }
    }
}

