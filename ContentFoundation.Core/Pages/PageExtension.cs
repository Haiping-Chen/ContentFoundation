using ContentFoundation.Core.Blocks;
using ContentFoundation.Core.Utility;
using CustomEntityFoundation;
using CustomEntityFoundation.Utilities;
using EntityFrameworkCore.BootKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentFoundation.Core.Pages
{
    public static class PageExtension
    {
        public static bool Add(this Page pageModel, Database dc)
        {
            if (dc.Table<Page>().Any(x => x.Id == pageModel.Id)) return false;

            dc.Table<Page>().Add(pageModel);

            if(pageModel.Blocks != null)
            {
                pageModel.Blocks.ForEach(block => {

                    /*if(!String.IsNullOrEmpty(block.Name))
                    {
                        var dmBlock = new DomainModel<BlockEntity>(pageModel.Dc, block);
                        dmBlock.AddEntity();
                    }*/
                    
                    var dm = new BlockInPage { BlockId = block.Id, PageId = pageModel.Id };
                    dm.Position = block.Position;

                    dc.Table<BlockInPage>().Add(dm);
                });
            }

            return true;
        }

        public static void Update(this Page pageModel, Database dc)
        {
            if (pageModel.Blocks != null)
            {
                pageModel.Blocks.ForEach(block => {

                    var existedBlock = dc.Table<Block>().Find(block.Id);
                    var pageBlock = dc.Table<BlockInPage>().FirstOrDefault(x => x.PageId == pageModel.Id && x.BlockId == block.Id);

                    if (existedBlock == null)
                    {
                        var dmBlock = block.ToObject<Block>();
                        dc.Table<Block>().Add(dmBlock);
                    }

                    if (pageBlock == null)
                    {
                        var dm = new BlockInPage { BlockId = block.Id, PageId = pageModel.Id };
                        dm.Position = block.Position;

                        dc.Table<BlockInPage>().Add(dm);
                    }
                    else
                    {
                        pageBlock.Position = block.Position;
                    }
                    
                });
            }
        }

        public static void Load(this Page pageModel, Database dc)
        {
            var blocks = (from b in dc.Table<Block>()
                          join pb in dc.Table<BlockInPage>() on b.Id equals pb.BlockId
                          where pb.PageId == pageModel.Id
                          select new { Id = pb.Id, Block = b, Position = pb.Position }).ToList();

            pageModel.Blocks = blocks.Select(x => {
                x.Block.Position = x.Position == null ? new BlockPositionInPage { H = 12, W = 12, X = 6, Y = 0 } : x.Position;
                return x.Block;
            }).ToList();
        }
    }
}
