﻿using SmartCity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartCity.WebUI.Models
{
    public class FourumIndexModel
    {
        /// <summary>
        /// 公告类
        /// </summary>
        public List<Notice> NewsItems { get; set; }
        /// <summary>
        /// 论坛类
        /// </summary>
        public List<Posts> PostsItems { get; set; }
        /// <summary>
        /// 热门论坛类
        /// </summary>
        public List<Posts> HotPostsItems { get; set; }
        /// <summary>
        /// 标签分布
        /// </summary>
        public List<PostsType> PostsTypeItems { get; set; }
        /// <summary>
        /// 最新评论
        /// </summary>
        public List<Review> LatestReviewsItems { get; set; }
        /// <summary>
        /// 当前评论
        /// </summary>
        public List<CurrentReview> CurrentReviewItems { get; set; }

        public string Title1 { get; set; }
        public string TitleUrL1 { get; set; }
        public string Tiltle2 { get; set; }

        public string TitleUrl2 { get; set; }
    }
}