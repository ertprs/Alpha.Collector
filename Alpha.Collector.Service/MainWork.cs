using Alpha.Collector.Core;
using Alpha.Collector.Dao;
using Alpha.Collector.Model;
using Alpha.Collector.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
        private const int SleepTime = 30 * 1000;
        private AlphaFileLog log;
        private bool isClear;
        private object locker = new object();

        public MainWork()
        {
            this.log = new AlphaFileLog();
            List<Lottery> list = new List<Lottery>();
            try
            {
                this.isClear = ConfigurationManager.AppSettings["IsClear"] == null ? false : bool.Parse(ConfigurationManager.AppSettings["IsClear"]);
            }
            catch (Exception ex)
            {
                this.log.Error($"初始化出错。详情：{ex.ToString()}");
            }
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Run()
        {
            this.DoClear();
            this.DoPick();
        }

        /// <summary>
        /// 执行抓取
        /// </summary>
        private void DoPick()
        {
            Task.Run(() =>
            {
                List<Type> managerList = ReflectionHelper.GetClasses<PickerManager>();//.Where(t => t.Name == "JSK3PickerManager").ToList();
                ParallelOptions options = new ParallelOptions { MaxDegreeOfParallelism = 10 };
                Parallel.ForEach(managerList, type =>
                {
                    try
                    {
                        PickerManager pickerManager = Activator.CreateInstance(type) as PickerManager;
                        while (true)
                        {
                            try
                            {
                                List<OpenResult> list = pickerManager.DoPick(TaskCount);
                                int result = OpenResultApp.Insert(list);
                                if (result < 0)
                                {
                                    lock (locker)
                                    {
                                        log.Error($"插入结果到数据库失败。数量：{list.Count}，类型：{type.ToString()}");
                                    }
                                }
                                Thread.Sleep(SleepTime);
                            }
                            catch (Exception ex)
                            {
                                lock (locker)
                                {
                                    this.log.Error($"抓取异常。详情：{ex.ToString()}");
                                }
                                Thread.Sleep(SleepTime);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        lock (locker)
                        {
                            this.log.Error($"抓取异常。详情：{ex.ToString()}");
                        }
                    }
                });
            });
        }

        /// <summary>
        /// 执行清除
        /// </summary>
        private void DoClear()
        {
            if (!isClear)
            {
                return;
            }

            Task.Run(() =>
        {
            while (true)
            {
                if (DateTime.Now.Hour != 0)
                {
                    Thread.Sleep(30 * 60 * 1000);
                }
                else
                {
                    OpenResultClear.Clear(30);
                    AppLogClear.Clear(30);
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
