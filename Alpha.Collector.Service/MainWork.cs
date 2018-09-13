using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Alpha.Collector.Service
{
    /// <summary>
    /// MainWork
    /// </summary>
    public class MainWork
    {
        private const int TaskCount = 10;
        private AlphaFileLog log = new AlphaFileLog();
        private const int SleepTime = 30 * 1000;
        private static string PickList = ConfigurationManager.AppSettings["PickList"] ?? "";

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Run()
        {
            if (PickList.Contains(LotteryType.AHK3))
            {
                this.PickAHK3();
            }

            if (PickList.Contains(LotteryType.BJK3))
            {
                this.PickBJK3();
            }

            if (PickList.Contains(LotteryType.BJKC))
            {
                this.PickBJKC();
            }

            if (PickList.Contains(LotteryType.BJKL8))
            {
                this.PickBJKL8();
            }

            if (PickList.Contains(LotteryType.CQSSC))
            {
                this.PickCQSSC();
            }

            if (PickList.Contains(LotteryType.CQXYNC))
            {
                this.PickCQXYNC();
            }

            if (PickList.Contains(LotteryType.FC3D))
            {
                this.PickFC3D();
            }

            if (PickList.Contains(LotteryType.GDKLSF))
            {
                this.PickGDKLSF();
            }

            if (PickList.Contains(LotteryType.GXKLSF))
            {
                this.PickGXKLSF();
            }

            if (PickList.Contains(LotteryType.GXK3))
            {
                this.PickGXK3();
            }

            if (PickList.Contains(LotteryType.HNKLSF))
            {
                this.PickHNKLSF();
            }

            if (PickList.Contains(LotteryType.HuBK3))
            {
                this.PickHuBK3();
            }

            if (PickList.Contains(LotteryType.JLK3))
            {
                this.PickJLK3();
            }

            if (PickList.Contains(LotteryType.JSK3))
            {
                this.PickJSK3();
            }

            if (PickList.Contains(LotteryType.PCDD))
            {
                this.PickPCDD();
            }

            if (PickList.Contains(LotteryType.TCPL3))
            {
                this.PickTCPL3();
            }

            if (PickList.Contains(LotteryType.TJSSC))
            {
                this.PickTJSSC();
            }

            if (PickList.Contains(LotteryType.XGLHC))
            {
                this.PickXGLHC();
            }

            if (PickList.Contains(LotteryType.XJSSC))
            {
                this.PickXJSSC();
            }
        }

        /// <summary>
        /// 抓取安徽快3
        /// </summary>
        private void PickAHK3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new AHK3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入安徽快3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取安徽快3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取北京快3
        /// </summary>
        private void PickBJK3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new BJK3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入北京快3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取北京快3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取北京赛车
        /// </summary>
        private void PickBJKC()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new BJKCPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入北京赛车结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取北京赛车异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取北京快乐8
        /// </summary>
        private void PickBJKL8()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new BJKL8PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入北京快乐8结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取北京快乐8异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取重庆时时彩
        /// </summary>
        private void PickCQSSC()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new CQSSCPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入重庆时时彩结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取重庆时时彩异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取重庆幸运农场
        /// </summary>
        private void PickCQXYNC()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new CQXYNCPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入重庆幸运农场结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取重庆幸运农场异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取福彩3D
        /// </summary>
        private void PickFC3D()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new FC3DPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入福彩3D结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取福彩3D异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取广东快乐十分
        /// </summary>
        private void PickGDKLSF()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new GDKLSFPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入广东快乐十分结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取广东快乐十分异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取广西快乐十分
        /// </summary>
        private void PickGXKLSF()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new GXKLSFPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入广西快乐十分结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取广西快乐十分异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取广西快3
        /// </summary>
        private void PickGXK3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new GXK3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入广西快3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取广西快3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取湖南快乐十分
        /// </summary>
        private void PickHNKLSF()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new HNKLSFPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入湖南快乐十分结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取湖南快乐十分异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取湖北快3
        /// </summary>
        private void PickHuBK3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new HuBK3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入湖北快3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取湖北快3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取吉林快3
        /// </summary>
        private void PickJLK3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new JLK3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入吉林快3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取吉林快3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取江苏快3
        /// </summary>
        private void PickJSK3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new JSK3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入江苏快3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取江苏快3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取PC蛋蛋
        /// </summary>
        private void PickPCDD()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new PCDDPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入PC蛋蛋结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取PC蛋蛋异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取体彩排列3
        /// </summary>
        private void PickTCPL3()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new TCPL3PickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入体彩排列3结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取体彩排列3异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取天津时时彩
        /// </summary>
        private void PickTJSSC()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new TJSSCPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入天津时时彩结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取天津时时彩异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取香港六合彩
        /// </summary>
        private void PickXGLHC()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new XGLHCPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入香港六合彩结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取香港六合彩异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 抓取新疆时时彩
        /// </summary>
        private void PickXJSSC()
        {
            Task.Run(() =>
            {
                IPickerManager manager = new XJSSCPickerManager();
                while (true)
                {
                    try
                    {
                        List<OpenResult> list = manager.DoPick(TaskCount);
                        int result = OpenResultApp.Insert(list);
                        if (result < 0)
                        {
                            log.Error($"插入新疆时时彩结果到数据库失败。数量：{list.Count}");
                        }
                        Thread.Sleep(SleepTime);
                    }
                    catch (Exception ex)
                    {
                        log.Error($"抓取新疆时时彩异常。详情：{ex.ToString()}");
                        Thread.Sleep(SleepTime);
                    }
                }
            });
        }

        /// <summary>
        /// 停止执行
        /// </summary>
        public void Stop()
        {

        }
    }
}
