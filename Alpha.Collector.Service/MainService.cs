using Alpha.Collector.Core;
using System.ServiceProcess;

namespace Alpha.Collector.Service
{
    public partial class MainService : ServiceBase
    {
        AlphaFileLog log = new AlphaFileLog();
        MainWork mainWork = new MainWork();

        public MainService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 服务启动时执行
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            mainWork.Run();
            this.log.Info("服务已成功启动！");
        }

        /// <summary>
        /// 服务停止时执行
        /// </summary>
        protected override void OnStop()
        {
            mainWork.Stop();
            this.log.Info("服务已成功停止！");
        }
    }
}
