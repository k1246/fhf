using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;
using Dapper;
namespace FlatUI_TestPlatform.PubCls
{
    public static  class MyDB
    {
        //定义数据库连接字符串，数据库不要设置密码，容易报错
        public static string conStr = @"Data Source=" + Application.StartupPath + "\\DB\\SQlite_DATA.db";
        public static SqlCommand sqlCommand;//常用Sql语句

        #region Dapper操作SQLite中《用户管理表》公共部分
        /// <summary>
        /// 定义用户数据表实体类，根据数据库表结构来进行更改
        /// </summary>
        public class UserModel
        {
            public Int32 ID { get; set; }
            public string 员工编号 { get; set; }//员工编号
            public string 姓名 { get; set; }//姓名
            public string 密码 { get; set; }//密码
            public string 权限 { get; set; }//权限
        }

        /// <summary>
        /// 查询所有用户数据
        /// </summary>
        /// <returns></returns>
        public static List<UserModel> MutiQueryUser()
        {
            //using:自动释放所新建的对象，避免缓存，内存溢出
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Open();
                var output = cnn.Query<UserModel>("select * from 登录权限表", new DynamicParameters());
                return output.ToList();
                //也可以一句来完成，代码如下：
                //return (List<UserModel>)cnn.Query<UserModel>("select * from 登录权限表");
            }
        }

        /// <summary>
        /// 查询指定用户数据
        /// </summary>
        /// <returns></returns>
        public static List<UserModel> QueryUser(UserModel userdata)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                var output = cnn.Query<UserModel>("select * from 登录权限表 where 员工编号 = @员工编号", userdata);
                return output.ToList();
            }
        }

        /// <summary>
        /// 插入数据，无返回值
        /// </summary>
        /// <param name="data"></param>
        public static void InsertUser(UserModel userdata)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Execute("insert into 登录权限表 (员工编号,姓名,密码,权限) values(@员工编号,@姓名,@密码,@权限)", userdata);
            }
        }

        /// <summary>
        /// 批量插入数据，返回影响行数
        /// </summary>
        /// <param name="datas"></param>
        /// <returns>影响行数</returns>
        public static int MutiInsertUserData(List<UserModel> userdatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("insert into 登录权限表 (员工编号,姓名,密码,权限) values(@员工编号,@姓名,@密码,@权限)", userdatas);
            }
        }

        /// <summary>
        /// 根据id更新用户数据
        /// </summary>
        /// <param name="data"></param>
        public static int UpdateUserData(UserModel userdata)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("update 登录权限表 set 员工编号 = @员工编号,姓名 = @姓名,密码 = @密码,权限 = @权限 where ID=@ID", userdata);
            }
        }

        /// <summary>
        /// 更新多组用户数据
        /// </summary>
        /// <param name="userdatas"></param>
        public static int MutiUpdateUserData(List<UserModel> userdatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("update 登录权限表  set 员工编号 = @员工编号,姓名 = @姓名,密码 = @密码,权限 = @权限 where ID=@ID", userdatas);
            }
        }

        /// <summary>
        /// 根据id删除用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteUser(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("delete from 登录权限表 where ID=" + id);
            }
        }

        /// <summary>
        /// 删除多组用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int MutiDeleteUser(List<UserModel> userdatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("delete from 登录权限表 where ID=@ID", userdatas);
            }
        }
        #endregion

        #region Dapper操作SQLite中《配方管理表》公共部分
        /// <summary>
        /// 定义用户数据表实体类，根据数据库表结构来进行更改
        /// </summary>
        public class RecipeModel
        {
            public Int32 ID { get; set; }
            public string 配方名称 { get; set; }
            public string 机型 { get; set; }
            public string 匹配尺寸 { get; set; }
            public string 公差上限 { get; set; }
            public string 公差下限 { get; set; }
        }

        /// <summary>
        /// 查询所有用户数据
        /// </summary>
        /// <returns></returns>
        public static List<RecipeModel> MutiQueryRecipe(string sqlCommand)
        {
            //using:自动释放所新建的对象，避免缓存，内存溢出
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Open();
                //var output = cnn.Query<RecipeModel>("select * from 配方1", new DynamicParameters());
                var output = cnn.Query<RecipeModel>(sqlCommand, new DynamicParameters());

                return output.ToList();
                //也可以一句来完成，代码如下：
                //return (List<UserModel>)cnn.Query<UserModel>("select * from 登录权限表");
            }
        }

        /// <summary>
        /// 查询指定用户数据
        /// </summary>
        /// <returns></returns>
        public static List<RecipeModel> QueryRecipe(RecipeModel recipeData)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                var output = cnn.Query<RecipeModel>("select * from 配方1 where 配方名称 = @配方名称", recipeData);
                return output.ToList();
            }
        }

        /// <summary>
        /// 插入数据，无返回值
        /// </summary>
        /// <param name="data"></param>
        public static void InsertRecipe(RecipeModel recipeData)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Execute("insert into 配方1 (配方名称,机型,匹配尺寸,公差上限,公差下限) values(@配方名称,@机型,@匹配尺寸,@公差上限,@公差下限)", recipeData);
            }
        }

        /// <summary>
        /// 批量插入数据，返回影响行数
        /// </summary>
        /// <param name="datas"></param>
        /// <returns>影响行数</returns>
        public static int MutiInsertRecipeData(List<RecipeModel> recipeDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("insert into 配方1 (配方名称,机型,匹配尺寸,公差上限,公差下限) values(@配方名称,@机型,@匹配尺寸,@公差上限,@公差下限)", recipeDatas);
            }
        }

        /// <summary>
        /// 根据id更新用户数据
        /// </summary>
        /// <param name="data"></param>
        public static int UpdateRecipeData(RecipeModel recipeData)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("update 配方1 set 配方名称 = @配方名称,机型 = @机型,匹配尺寸 = @匹配尺寸,公差上限 = @公差上限,公差下限 = @公差下限 where ID=@ID", recipeData);
            }
        }

        /// <summary>
        /// 更新多组用户数据
        /// </summary>
        /// <param name="userdatas"></param>
        public static int MutiUpdateRecipeData(List<RecipeModel> recipeDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("update 配方1  set 配方名称 = @配方名称,机型 = @机型,匹配尺寸 = @匹配尺寸,公差上限 = @公差上限,公差下限 = @公差下限 where ID=@ID", recipeDatas);
            }
        }

        /// <summary>
        /// 根据id删除配方数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteRecipe(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("delete from 配方1 where ID=" + id);
            }
        }

        /// <summary>
        /// 删除多组用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int MutiDeleteRecipe(List<RecipeModel> recipeDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("delete from 配方1 where ID=@ID", recipeDatas);
            }
        }
        #endregion

        #region Dapper操作SQLite中《结果管理表》公共部分
        /// <summary>
        /// 定义用户数据表实体类，根据数据库表结构来进行更改
        /// </summary>
        public class ReviewModel
        {
            public Int32 ID { get; set; }
            //public Int32 序号 { get; set; }
            public string 产品名称 { get; set; }
            public string 机型 { get; set; }
            public string 流水号 { get; set; }
            public string 操作者 { get; set; }
            public string 结果值 { get; set; }
            public string 填隙片规格 { get; set; }
            public string 日期时间 { get; set; }
        }

        /// <summary>
        /// 查询所有用户数据
        /// </summary>
        /// <returns></returns>
        public static List<ReviewModel> MutiQueryReview()
        {
            //using:自动释放所新建的对象，避免缓存，内存溢出
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Open();
                var output = cnn.Query<ReviewModel>("select * from 结果", new DynamicParameters());
                return output.ToList();
                //也可以一句来完成，代码如下：
                //return (List<UserModel>)cnn.Query<UserModel>("select * from 结果");
            }
        }

        /// <summary>
        /// 查询指定用户数据
        /// </summary>
        /// <returns></returns>
        public static List<ReviewModel> QueryReview(ReviewModel reviewData)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                var output = cnn.Query<ReviewModel>("select * from 结果 where 产品名称 = @产品名称", reviewData);
                return output.ToList();
            }
        }

        /// <summary>
        /// 插入数据，无返回值
        /// </summary>
        /// <param name="data"></param>
        public static void InsertReview(ReviewModel reviewData)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Execute("insert into 结果 (产品名称,机型,流水号,操作者,结果值,填隙片规格,日期时间) values(@产品名称,@机型,@流水号,@操作者,@结果值,@填隙片规格,@日期时间)", reviewData);
            }
        }

        /// <summary>
        /// 批量插入数据，返回影响行数
        /// </summary>
        /// <param name="datas"></param>
        /// <returns>影响行数</returns>
        public static int MutiInsertReviewData(List<ReviewModel> reviewDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("insert into 结果 (产品名称,机型,流水号,操作者,结果值,填隙片规格,日期时间) values(@产品名称,@机型,@流水号,@操作者,@结果值,@填隙片规格,@日期时间)", reviewDatas);
            }
        }

        /// <summary>
        /// 根据id更新用户数据
        /// </summary>
        /// <param name="data"></param>
        public static int UpdateReviewData(ReviewModel reviewData)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("update 结果 set 产品名称 = @产品名称,机型 = @机型,流水号 = @流水号,操作者 = @操作者,结果值 = @结果值,填隙片规格 = @填隙片规格,日期时间 = @日期时间 where ID=@ID", reviewData);
            }
        }

        /// <summary>
        /// 更新多组用户数据
        /// </summary>
        /// <param name="userdatas"></param>
        public static int MutiUpdateReviewData(List<ReviewModel> reviewDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("update 结果 set 产品名称 = @产品名称,机型 = @机型,流水号 = @流水号,操作者 = @操作者,结果值 = @结果值,填隙片规格 = @填隙片规格,日期时间 = @日期时间 where ID=@ID", reviewDatas);
            }
        }

        /// <summary>
        /// 根据id删除配方数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteReview(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("delete from 结果 where ID=" + id);
            }
        }

        /// <summary>
        /// 删除多组用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int MutiDeleteReview(List<ReviewModel> reviewDatas)
        {
            using (IDbConnection cnn = new SQLiteConnection(conStr))
            {
                return cnn.Execute("delete from 结果 where ID=@ID", reviewDatas);
            }
        }
        #endregion

        #region 数据库管理
        //执行数据库压缩指令
        public static void ExecuteZip()
        {
            using(IDbConnection cnn = new SQLiteConnection(conStr))
            {
                cnn.Execute("VACUUM");
            }
        }
        #endregion

        public struct SqlCommand//Sql语句结构体
        {
            public string Reserve01;//预留
            public string Reserve02;//预留
            public string Reserve03;//预留
            public string Reserve04;//预留
            public string Reserve05;//预留
            public string Reserve06;//预留
            public string Reserve07;//预留
            public string Reserve08;//预留
            public string Reserve09;//预留
            public string Reserve10;//预留
        }
    }
}
