﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NUnit.Framework;

using Gnosis.Alexandria.Loggers;
using Gnosis.Core;
using Gnosis.Core.Tags.Id3.Id3v2;
using Gnosis.Data.SQLite;

namespace Gnosis.Tests.Data.SQLite
{
    public abstract class MediaDetailTestBase
    {
        protected MediaDetailTestBase()
        {
            connection = new SQLiteConnectionFactory().Create("Data Source=:memory:;Version=3;");
            connection.Open();
            tagRepository = new SQLiteTagRepository(logger, tagTypeFactory, connection);
            linkRepository = new SQLiteLinkRepository(logger, linkTypeFactory, connection);
            detailRepository = new MediaDetailRepository(logger, tagRepository, linkRepository);

            tagRepository.Initialize();
            linkRepository.Initialize();

            InitializeData();
        }

        private readonly IDbConnection connection;
        private readonly ILinkTypeFactory linkTypeFactory = new LinkTypeFactory();
        private readonly ITagTypeFactory tagTypeFactory = new TagTypeFactory();
        protected readonly ILogger logger = new DebugLogger();
        protected readonly ITagRepository tagRepository;
        protected readonly ILinkRepository linkRepository;
        protected readonly IMediaDetailRepository detailRepository;

        protected void Cleanup()
        {
            connection.Close();
        }

        #region Test Values

        protected void InitializeData()
        {
            link1 = new Link(trackUri1, thumbUri1, LinkType.ThumbnailImage, "Album Cover Image");
            linkRepository.Save(new List<ILink> { link1 });
            System.Diagnostics.Debug.WriteLine("Save Link");

            tag1 = new Tag(trackUri1, Id3v2TagType.Artist, new string[] { "Nine Inch Nails", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty });
            tagRepository.Save(new List<ITag> { tag1 });
            System.Diagnostics.Debug.WriteLine("Save Tag");
        }

        protected Uri trackUri1 = new Uri("http://example.com/bands/nin/broken/pinion.mp3");
        protected Uri thumbUri1 = new Uri("http://example.com/images/nin/broken/cover.jpg");
        protected ILink link1;
        protected ITag tag1;

        #endregion
    }

    [TestFixture]
    public class Saved_Tags_And_Links
        : MediaDetailTestBase
    {
        [Test]
        public void Can_Be_Read_As_Details()
        {
            var details = new List<IMediaDetail>();
            var completed = false;
            var request = new MediaDetailRequest("Nine%", item => details.Add(item), () => completed = true);

            detailRepository.Search(request);

            while (!completed)
            {
                System.Threading.Thread.Sleep(100);
            }

            Assert.AreEqual(1, details.Count);
            var first = details.FirstOrDefault();
            Assert.IsNotNull(first);
            Assert.AreEqual(tag1.Value, first.Tag.Value);
            Assert.AreEqual(link1.Target, first.Thumbnail.Location);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Cleanup();
        }
    }
}
