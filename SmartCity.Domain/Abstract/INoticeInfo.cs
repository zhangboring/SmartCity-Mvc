﻿using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCity.Domain.Abstract
{
    public interface INoticeInfo
    {
        /// <summary>
        ///获取用户信息集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<Notice> GetNewsList();
        /// <summary>
        /// 添加新闻通告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool AddNews(Notice model);
        /// <summary>
        /// 修改公告状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        bool EditPublishStatu(int ID, int Status);
        /// <summary>
        ///获取公告信息集合
        /// </summary>
        /// <returns></returns>
         IEnumerable<Notice> GetNoticeInfoByID(int id);
        /// <summary>
        /// 得到最新的通知
        /// </summary>
        /// <returns></returns>
        IEnumerable<Notice> GetNewsListByPublished();
        /// <summary>
        /// 修改公告
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        bool EditNewsInfo(Notice model);
        /// <summary>
        /// 查询公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        IEnumerable<Notice> SearchContent(int NewsID);
        /// <summary>
        /// 删除公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        bool DeleteNews(int NewsID);
        /// <summary>
        /// 查询公告内容
        /// </summary>
        /// <param name="NewsID"></param>
        /// <returns></returns>
        IEnumerable<Notice> SerachNewsByNewsName(string NewsName, DateTime? startTime, DateTime? endTime);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
         bool BatchRemoveNoticeInfo(List<int> id);
    }
}
