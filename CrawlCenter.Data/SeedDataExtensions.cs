using System;
using System.Collections.Generic;
using CrawlCenter.Data.Models;
using FreeSql;

namespace CrawlCenter.Data {
    public static class SeedDataExtensions {
        private static readonly ProjectTag Tag1 = new ProjectTag {
            Id = Guid.NewGuid().ToString(),
            Name = "业务中台"
        };

        private static readonly Project Project1 = new Project {
            Id = Guid.NewGuid().ToString(),
            Name = "业务运营平台",
            ProjectTags = new List<ProjectTag> {Tag1}
        };

        private static readonly Project Project2 = new Project {
            Id = Guid.NewGuid().ToString(),
            Name = "OA",
            ProjectTags = new List<ProjectTag> {Tag1}
        };

        private static readonly List<ProjectTag> ProjectTags = new List<ProjectTag> {Tag1};

        private static readonly List<Project> Projects = new List<Project> {Project1, Project2};

        private static readonly List<CrawlTask> CrawlTasks = new List<CrawlTask> {
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "监控中心 - 订单查询 - 归属",
                Description = "订单归属",
                ProjectId = Project1.Id,
                Cmd = "python3 main.py"
            },
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "监控中心 - 订单查询 - 交付区域",
                Description = "交付区域",
                ProjectId = Project1.Id,
                Cmd = "python3 main.py"
            },
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "订单中心 - 查询订单 - 订单数据查询",
                Description = "订单数据查询",
                ProjectId = Project1.Id,
                Cmd = "python3 main.py"
            },
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "监控中心 - 交付统计 - 订单交付明细查询",
                Description = "订单交付明细查询",
                ProjectId = Project1.Id,
                Cmd = "python3 main.py"
            },
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "交付中心 - 码上购2.0 - 派送人员审核",
                Description = "派送人员审核",
                ProjectId = Project1.Id,
                Cmd = "python3 main.py"
            },
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "报表中心 - 明细专题 - 订单明细",
                Description = "订单明细",
                ProjectId = Project1.Id,
                Cmd = "python3 main.py"
            },
            new CrawlTask {
                Id = Guid.NewGuid().ToString(),
                Name = "OA - 智慧协作云 - 环节时效报表",
                Description = "环节时效报表",
                ProjectId = Project2.Id,
                Cmd = "python3 main.py"
            }
        };

        public static void SeedData(this ICodeFirst codefirst) {
            codefirst.Entity<ProjectTag>(eb => eb.HasData(ProjectTags));
            codefirst.Entity<Project>(eb => eb.HasData(Projects));
            codefirst.Entity<CrawlTask>(eb => eb.HasData(CrawlTasks));
        }
    }
}