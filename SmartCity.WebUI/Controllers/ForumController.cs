﻿using SmartCity.Common;
using SmartCity.Common.log4net.Ext;
using SmartCity.Domain.Abstract;
using SmartCity.Domain.Entities;
using SmartCity.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartCity.WebUI.Controllers
{
    public class ForumController : BaseController
    {
        #region 字段 构造函数
        private INoticeInfo NewsInfoService;
        private IPostsInfo PostInfoService;
        private IUserInfo UserInfoService;
        private IReviewInfo ReviewInfoService;
        private IReplyInfo ReplyInfoService;
        public ForumController(INoticeInfo NewsInfo, IPostsInfo PostInfo, IUserInfo UserInfos, IReviewInfo ReviewInfo, IReplyInfo ReplyInfo)
        {
            this.NewsInfoService = NewsInfo;
            this.PostInfoService = PostInfo;
            this.UserInfoService = UserInfos;
            this.ReviewInfoService = ReviewInfo;
            this.ReplyInfoService = ReplyInfo;
        }
        #endregion

        // GET: Forum
        public ActionResult FourumIndex(int ID)
        {
            var model = CurrentUserInfo;
            var result = PostInfoService.AddNumberForWatch(ID);
            //获取通知公告
            var Model = new FourumIndexModel();
            //获取论坛
            var PostsModel = PostInfoService.GetPostsInfoByID(ID).ToList();
            //获取热门帖子
            var HotPostsModel = PostInfoService.GetHotPostsInfo().ToList();
            //获取标签
            var PostsType = PostInfoService.SerachPostsType().ToList();
            //获取最新评论
            var LatestReviews = ReviewInfoService.GetLatestReviews().ToList();
            //显示当前评论
            var CurrentReviews = ReviewInfoService.GetLatestReviewsByID(ID).ToList();

            var CurrentList = new List<CurrentReview>();
            foreach (var item in CurrentReviews)
            {
                var CurrentModel = new CurrentReview();
                CurrentModel.ReviewID = item.ReviewID;
                CurrentModel.ReviewContent = item.ReviewContent;
                CurrentModel.CreateTime = item.CreateTime;
                CurrentModel.PostsModel = item.PostsModel;
                CurrentModel.UserModel = item.UserModel;
                List<Reply> ReplyList = ReplyInfoService.GetLatestReviews(item.ReviewID).ToList();
                CurrentModel.ReplyModel = null;
                if (ReplyList != null)
                {
                    CurrentModel.ReplyModel = ReplyList;
                }
                CurrentList.Add(CurrentModel);
            }
            Model.CurrentReviewItems = CurrentList;
            Model.HotPostsItems = HotPostsModel;
            Model.PostsItems = PostsModel;
            Model.PostsTypeItems = PostsType;
            Model.LatestReviewsItems = LatestReviews;
            Model.Title1 = "Hi, 请登录";
            Model.Tiltle2 = "我要注册";
            Model.TitleUrL1 = "#";
            Model.TitleUrl2 = "#";
            if (model != null)
            {
                var models = model as User;
                Model.Title1 = "Hi, 欢迎你";
                Model.Tiltle2 = models.UserName;
            }
            return View(Model);
        }
       /// <summary>
       /// 评论添加
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
        [HttpPost]
        public ActionResult ReviewAdd(Review model)
        {
            if (CurrentUserInfo!=null)
            {
                model.CreateTime = DateTime.Now;
                model.UserID = CurrentUserInfo.OwnerID;
                var result= ReviewInfoService.AddReview(model);
                if (result)
                {
                    var result1 = PostInfoService.AddNumberForReview(model.ForumID);
                    if (result1)
                    {
                        return Json(new { IsSuccess = 0, Message = "评论成功！" });
                    }
                    return Json(new { IsSuccess = 1, Message = "评论失败，请稍后重试！" });
                }
                return Json(new { IsSuccess = 1, Message = "评论失败，请稍后重试！" });
            }
            return Json(new { IsSuccess = 1, Message = "对不起，你还未登陆，不能进行评论！" });
        }
        /// <summary>
        /// 回复添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult ReplyAdd(Reply model)
        {
            if (CurrentUserInfo != null)
            {
                model.CreateTime = DateTime.Now;
                model.UserID = CurrentUserInfo.OwnerID;
                var result = ReplyInfoService.AddReply(model);
                if (result)
                {
                    return Json(new { IsSuccess = 0, Message = "回复成功！" });
                }
                return Json(new { IsSuccess = 1, Message = "回复失败，请稍后重试！" });
            }
            return Json(new { IsSuccess = 1, Message = "对不起，你还未登陆，不能进行评论！" });
        }

        public ActionResult PostsAdd(string Title,string Content,string PostsLable)
        {
            var model =new Posts();
            model.PostsTitle = Title;
            model.PostsLable = PostsLable;
            model.Contents = Content;
            model.BriefContent = Content;
            model.TimesWatched = 0;
            model.CommentsNumber = 0;
            model.CreateTime = DateTime.Now;
            model.UserID = CurrentUserInfo.OwnerID;

            return View();
        }

    }
}