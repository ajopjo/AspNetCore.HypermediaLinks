﻿using AspNetCore.HypermediaLinks.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AspNetCore.HypermediaLinks.Tests
{
    public class ConfigurationLinkbuilderTest
    {
        private readonly List<LinkConfiguration> _configs = new List<LinkConfiguration>()
        {
            new LinkConfiguration(){
                Name="modeltest",
                Uri=new Uri("https://configtest.com"),
                Path="modeltest/{Id}",
                Type="GET"
            },
            new LinkConfiguration()
        };

        [Fact]
        public void ModelLinkTest()
        {
            var model = new MoqConfigModel();
            model.AddHyperMediaSupportLinks(new HypermediaBuilder(_configs));
            Assert.Equal(1, model.Links.Count());
            Assert.Equal("self", model.Links.FirstOrDefault().Rel);
            Assert.Equal("GET", model.Links.FirstOrDefault().Type);
            Assert.Equal(new Uri("https://configtest.com/modeltest/1"), model.Links.FirstOrDefault().Href);
        }

    }

    class MoqConfigModel : HyperMediaSupportModel
    {
        public int Id { get; set; }
        public override void AddHypermediaLinks(HypermediaBuilder builder)
        {
            Add(builder.FormConfiguration("modeltest", new { Id = 1 }).Build().AddSelfRel());
        }
    }

}
