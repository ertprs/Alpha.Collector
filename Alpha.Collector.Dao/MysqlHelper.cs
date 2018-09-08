using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;

namespace Alpha.Collector.Dao
{
    /// <summary>
    /// MysqlHelper
    /// </summary>
    public static class MySqlHelper
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static readonly string DBConnectionString = ConfigurationManager.ConnectionStrings["DBConnectString"].ToString();

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param = null, int? timeout = null)
        {
            try
            {
                using (var conn = ConnectionFactory.CreateInstance(DBConnectionString))
                {
                    return conn.Execute(sql, param, null, timeout);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 执行sql 事务型
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int ExcuteTransaction(string sql, object param = null)
        {
            try
            {
                using (var conn = ConnectionFactory.CreateInstance(DBConnectionString))
                {
                    IDbTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        conn.Execute(sql, param, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return -1;
                    }

                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="buffered"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IList<T> GetList<T>(string sql, object param = null, IDbTransaction trans = null, bool buffered = true, int? timeout = null)
        {
            try
            {
                using (var conn = ConnectionFactory.CreateInstance(DBConnectionString))
                {
                    return conn.Query<T>(sql, param, trans, buffered, timeout).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 带有Inner join的sql语句获取对象列表
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="map"></param>
        /// <param name="param"></param>
        /// <param name="splitOn"></param>
        /// <returns></returns>
        public static IList<TReturn> GetList<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "F_Id")
        {
            try
            {
                using (var conn = ConnectionFactory.CreateInstance(DBConnectionString))
                {
                    return conn.Query<TFirst, TSecond, TReturn>(sql, map, param, null, false, splitOn).ToList();
                }
            }
            catch (Exception e)
            {
                return new List<TReturn>();
            }
        }

        /// <summary>
        /// 分页获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pagerInfo"></param>
        /// <returns></returns>
        public static List<T> GetListPagination<T>(PagerInfo pagerInfo)
        {
            try
            {
                using (var conn = ConnectionFactory.CreateInstance(DBConnectionString))
                {
                    var param = new DynamicParameters();
                    param.Add("@tbName", pagerInfo.TableName);
                    param.Add("@pkField", pagerInfo.PKField);
                    param.Add("@pageIndex", pagerInfo.page);
                    param.Add("@pageSize", pagerInfo.rows);
                    param.Add("@returnField", pagerInfo.ReturnField);
                    param.Add("@orderField", pagerInfo.sidx);
                    param.Add("@queryStr", pagerInfo.QueryString);
                    param.Add("@itemCount", 0, DbType.Int32, ParameterDirection.Output);
                    param.Add("@pageCount", 0, DbType.Int32, ParameterDirection.Output);

                    var list = conn.Query<T>("SP_Pagination", param, commandType: CommandType.StoredProcedure).ToList();
                    pagerInfo.records = param.Get<int>("@itemCount");

                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="trans"></param>
        /// <param name="buffered"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static T GetOne<T>(string sql, object param = null, IDbTransaction trans = null, bool buffered = true, int? timeout = null)
        {
            try
            {
                using (var conn = ConnectionFactory.CreateInstance(DBConnectionString))
                {
                    var list = conn.Query<T>(sql, param, trans, buffered, timeout).ToList();
                    return list.Count == 0 ? default(T) : list[0];
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}