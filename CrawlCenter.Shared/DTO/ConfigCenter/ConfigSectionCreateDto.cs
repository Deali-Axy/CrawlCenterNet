﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CrawlCenter.Shared.DTO.ConfigCenter;

public class ConfigSectionCreateDto {
    [Display(Name = "名称")]
    [Required(ErrorMessage = "请输入名称，名称不能为空")]
    public string Name { get; set; }

    [Display(Name = "描述")]
    public string Description { get; set; }
}