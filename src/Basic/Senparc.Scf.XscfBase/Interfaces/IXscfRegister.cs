﻿using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Senparc.CO2NET.RegisterServices;
using Senparc.Scf.Core.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Scf.XscfBase
{
    public interface IXscfRegister
    {
        /// <summary>
        /// 是否可安装（但不影响执行注册代码）
        /// </summary>
        bool CanInstall { get; }
        /// <summary>
        /// 模块名称，要求全局唯一
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 编号，要求全局唯一
        /// </summary>
        string Uid { get; }
        /// <summary>
        /// 版本号
        /// </summary>
        string Version { get; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        string MenuName { get; }
        /// <summary>
        /// Icon图标
        /// </summary>
        string Icon { get; }
        /// <summary>
        /// 说明
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 注册方法，注册的顺序决定了界面中排列的顺序
        /// </summary>
        IList<Type> Functions { get; }

        /// <summary>
        /// 添加 AutoMap 映射
        /// </summary>
        ConcurrentBag<Action<Profile>> AutoMapMappingConfigs { get; set; }

        /// <summary>
        /// 安装代码
        /// </summary>
        Task InstallOrUpdateAsync(IServiceProvider serviceProvider, InstallOrUpdate installOrUpdate);
        /// <summary>
        /// 卸载代码
        /// </summary>
        Task UninstallAsync(IServiceProvider serviceProvider, Func<Task> unsinstallFunc);

        /// <summary>
        /// 获取首页Url
        /// <para>仅限实现了 IAreaRegister 接口之后的 Register，否则将返回 null</para>
        /// </summary>
        /// <returns></returns>
        string GetAreaHomeUrl();

        /// <summary>
        /// 获取 Area 其他页面的 URL
        /// </summary>
        /// <param name="path">URL 路径（不带 uid 参数）</param>
        /// <returns></returns>
        string GetAreaUrl(string path);

        /// <summary>
        /// 在 ConfigureServices 启动时注册当前模块
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <returns></returns>
        IServiceCollection AddXscfModule(IServiceCollection services);

        /// <summary>
        /// 添加AutoMap的映射关系
        /// </summary>
        /// <param name="mapping"></param>
        void AddAutoMapMapping(Action<Profile> mapping);

        /// <summary>
        /// 在 startup.cs 的 Configure() 方法中执行配置
        /// </summary>
        /// <param name="app"></param>
        /// <param name="registerService">CO2NET 注册对象</param>
        /// <returns></returns>
        IApplicationBuilder UseXscfModule(IApplicationBuilder app, IRegisterService registerService);

        /// <summary>
        /// 获取 EF Code First MigrationHistory 数据库表名
        /// </summary>
        /// <returns></returns>
        string GetDatabaseMigrationHistoryTableName();

      
    }
}
