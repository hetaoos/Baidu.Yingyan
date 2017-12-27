using System.Threading.Tasks;

namespace Baidu.Yingyan.Analysis
{
    /// <summary>
    /// 鹰眼轨迹分析类接口提供停留点分析和驾驶行为分析功能：
    /// <a href="http://lbsyun.baidu.com/index.php?title=yingyan/api/v3/analysis">轨迹分析</a>
    /// </summary>
    public partial class AnalysisApi
    {
        private YingyanApi framework;
        private const string url = "analysis/";

        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisApi"/> class.
        /// </summary>
        /// <param name="framework">The framework.</param>
        public AnalysisApi(YingyanApi framework)
        {
            this.framework = framework;
        }

        /// <summary>
        /// 停留点分析
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<TrackAnalysisStayPointResult> staypoint(TrackAnalysisStayPointParam param)
        {
            return framework.get<TrackAnalysisStayPointResult>(url + "staypoint", param);
        }

        /// <summary>
        /// 驾驶行为分析
        /// </summary>
        /// <param name="param">参数</param>
        /// <returns></returns>
        public Task<TrackAnalysisDrivingBehaviorResult> drivingbehaviour(TrackAnalysisDrivingBehaviorParam param)
        {
            return framework.get<TrackAnalysisDrivingBehaviorResult>(url + "drivingbehaviour", param);
        }
    }
}