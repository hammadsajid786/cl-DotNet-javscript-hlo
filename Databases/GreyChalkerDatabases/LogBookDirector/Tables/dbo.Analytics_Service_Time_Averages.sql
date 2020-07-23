CREATE TABLE [dbo].[Analytics_Service_Time_Averages]
(
[id] [int] NULL,
[rowguid] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [MSmerge_df_rowguid_D1752EE882B646948077E93C181423F7] DEFAULT (newsequentialid()),
[Test_Column] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Test_Column_Greg] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS OFF
GO
create trigger [MSmerge_del_D0273A169FAA46CDBBF406744E1C214B] on [LogBookDirector].[dbo].[Analytics_Service_Time_Averages] FOR DELETE   AS 
		declare @is_mergeagent bit, @at_publisher bit, @retcode smallint 

		set rowcount 0
	    set transaction isolation level read committed

		select @is_mergeagent = convert(bit, sessionproperty('replication_agent'))
		select @at_publisher = 0 
		declare @article_rows_deleted int
		select @article_rows_deleted = count(*) from deleted
		if @article_rows_deleted=0
			return
		declare @tablenick int, @replnick binary(6), 
				@lineage varbinary(311), @newgen bigint, @oldmaxversion int,
				@rowguid uniqueidentifier 
		declare @dt datetime, @nickbin varbinary(8), @error int
	     
		set nocount on
		select @tablenick = 6299000    
		if @article_rows_deleted = 1 select @rowguid = rowguidcol from deleted
		select @oldmaxversion= maxversion_at_cleanup from dbo.sysmergearticles where nickname = @tablenick
		select @dt = getdate()

		select @replnick = 0x38763b91b5ef  
		set @nickbin= @replnick + 0xFF

		select @newgen = NULL
		select top 1 @newgen = generation from [dbo].[MSmerge_genvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock, updlock, readpast) 
			where art_nick = 6299000          
				  and genstatus = 0
		if @newgen is NULL
		begin
			insert into [dbo].[MSmerge_genvw_D0273A169FAA46CDBBF406744E1C214B]  with (rowlock)
				(guidsrc, genstatus, art_nick, nicknames, coldate, changecount)
				 values (newid(), 0, @tablenick, @nickbin, @dt, @article_rows_deleted)
			select @error = @@error, @newgen = @@identity    
			if @error<>0 or @newgen is NULL
				goto FAILURE
		end  
		set @lineage = { fn UPDATELINEAGE(0x0, @replnick, @oldmaxversion+1) }  
		if @article_rows_deleted = 1
			insert into [dbo].[MSmerge_tsvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock) (rowguid, tablenick, type, lineage, generation)
				select @rowguid, @tablenick, 1, isnull((select { fn UPDATELINEAGE(COALESCE(c.lineage, @lineage), @replnick, @oldmaxversion+1) } from 
				[dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] c with (rowlock) where c.tablenick = @tablenick and c.rowguid = @rowguid),@lineage), @newgen
		else
			insert into [dbo].[MSmerge_tsvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock) (rowguid, tablenick, type, lineage, generation)
				select d.rowguidcol, @tablenick, 1, { fn UPDATELINEAGE(COALESCE(c.lineage, @lineage), @replnick, @oldmaxversion+1) }, @newgen from 
				deleted d left outer join [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] c with (rowlock) on c.tablenick = @tablenick and c.rowguid = d.rowguidcol 
	             
		if @@error <> 0
			GOTO FAILURE  
			delete [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B]  with (rowlock)
			from deleted d, [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] cont with (rowlock)
			where cont.tablenick = @tablenick and cont.rowguid = d.rowguidcol
			option (force order, loop join)

		if @@error <> 0
			GOTO FAILURE

	    
		return
	FAILURE:
		if @@trancount > 0
			rollback tran
		raiserror (20041, 16, -1)
		return 

		
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS OFF
GO
create trigger [MSmerge_ins_D0273A169FAA46CDBBF406744E1C214B] on [LogBookDirector].[dbo].[Analytics_Service_Time_Averages] for insert   as 
		declare @is_mergeagent bit, @retcode smallint 

		set rowcount 0
	    set transaction isolation level read committed

		select @is_mergeagent = convert(bit, sessionproperty('replication_agent'))
	        
		if (select trigger_nestlevel()) = 1 and @is_mergeagent = 1
			return  
		declare @article_rows_inserted int
		select @article_rows_inserted =  count(*) from inserted 
		if @article_rows_inserted = 0 
			return
		declare @tablenick int, @rowguid uniqueidentifier
		, @replnick binary(6), @lineage varbinary(311), @colv1 varbinary(1), @cv varbinary(1)
		, @ccols int, @newgen bigint, @version int, @curversion int
		, @oldmaxversion int, @ts_rows_exist bit
		declare @dt datetime
		declare @nickbin varbinary(8)
		declare @error int 
		set nocount on
		set @tablenick = 6299000
		set @lineage = 0x0
		set @retcode = 0
		select @oldmaxversion= maxversion_at_cleanup from dbo.sysmergearticles where nickname = @tablenick
		select @dt = getdate()

		select @replnick = 0x38763b91b5ef  
		set @nickbin= @replnick + 0xFF

		select @newgen = NULL
		select top 1 @newgen = generation from [dbo].[MSmerge_genvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock, updlock, readpast) 
			where art_nick = 6299000     and 
				  genstatus = 0
		if @newgen is NULL
		begin
			insert into [dbo].[MSmerge_genvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock)
				(guidsrc, genstatus, art_nick, nicknames, coldate, changecount)
				   values   (newid(), 0, @tablenick, @nickbin, @dt, @article_rows_inserted)
			select @error = @@error, @newgen = @@identity    
			if @error<>0 or @newgen is NULL
				goto FAILURE
		end
		set @lineage = { fn UPDATELINEAGE (0x0, @replnick, 1) }
				set @colv1 = NULL
		if (@@error <> 0)
		begin
			goto FAILURE
		end

		select @ts_rows_exist = 0 
			select @ts_rows_exist = 1 where exists (select ts.rowguid from inserted i, [dbo].[MSmerge_tsvw_D0273A169FAA46CDBBF406744E1C214B] ts with (rowlock) where ts.tablenick = @tablenick and ts.rowguid = i.rowguidcol)     
		if @ts_rows_exist = 1
		begin    
			select @version = max({fn GETMAXVERSION(lineage)}) from [dbo].[MSmerge_tsvw_D0273A169FAA46CDBBF406744E1C214B] where 
				tablenick = @tablenick and rowguid in (select rowguidcol from inserted) 

			if @version is not null
			begin
				-- reset lineage and colv to higher version...
				set @curversion = 0
				while (@curversion <= @version)
				begin
					set @lineage = { fn UPDATELINEAGE (@lineage, @replnick, @oldmaxversion+1) }
					set @curversion= { fn GETMAXVERSION(@lineage) }
				end

				if (@colv1 IS NOT NULL)
					set @colv1 = { fn UPDATECOLVBM(@colv1, @replnick, 0x01, 0x00, { fn GETMAXVERSION(@lineage) }) }    
					delete from [dbo].[MSmerge_tsvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock) where tablenick = @tablenick and rowguid in
						(select rowguidcol from inserted) 
			end
		end 
		
			insert into [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] with (rowlock) (tablenick, rowguid, lineage, colv1, generation, partchangegen, marker) 
			select @tablenick, rowguidcol, @lineage, @colv1, @newgen, (-@newgen), NULL
			from inserted i where not exists
			(select rowguid from [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] with (readcommitted, rowlock, readpast) where tablenick = @tablenick and rowguid = i.rowguidcol)  
		if @@error <> 0 
			goto FAILURE   

		return
	FAILURE:
		if @@trancount > 0
			rollback tran
		raiserror (20041, 16, -1)
		return 
		
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS OFF
GO
create trigger [MSmerge_upd_D0273A169FAA46CDBBF406744E1C214B] on [LogBookDirector].[dbo].[Analytics_Service_Time_Averages] FOR UPDATE   AS 
		declare @is_mergeagent bit, @at_publisher bit, @retcode int 

		set rowcount 0
	    set transaction isolation level read committed

		select @is_mergeagent = convert(bit, sessionproperty('replication_agent'))
		select @at_publisher = 0   
		declare @article_rows_updated int
		select @article_rows_updated = count(*) from inserted 
	    
		if @article_rows_updated=0
			return
		declare @contents_rows_updated int, @updateerror int, @rowguid uniqueidentifier
		, @bm varbinary(500), @missingbm varbinary(500), @lineage varbinary(311), @cv varbinary(1)
		, @tablenick int, @partchange int, @joinchange int, @logicalrelationchange int, @oldmaxversion int
		, @partgen bigint, @newgen bigint, @replnick binary(6)
		declare @dt datetime
		declare @nickbin varbinary(8)
		declare @error int 
		set nocount on

		set @tablenick = 6299000    
	    
		select @replnick = 0x38763b91b5ef   
		select @nickbin = @replnick + 0xFF
	    
		select @oldmaxversion = maxversion_at_cleanup from dbo.sysmergearticles where nickname = @tablenick
		select @dt = getdate()
	    
		-- Use intrinsic funtion to set bits for updated columns
		set @bm = columns_updated() 
		select @newgen = NULL
		select top 1 @newgen = generation from [dbo].[MSmerge_genvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock, updlock, readpast) 
			where art_nick = 6299000     and 
				genstatus = 0 
		if @newgen is NULL
		begin
			insert into [dbo].[MSmerge_genvw_D0273A169FAA46CDBBF406744E1C214B] with (rowlock)
			(guidsrc, genstatus, art_nick, nicknames, coldate, changecount)
				   values   (newid(), 0, @tablenick, @nickbin, @dt, @article_rows_updated)
			select @error = @@error, @newgen = @@identity    
			if @error<>0 or @newgen is NULL
				goto FAILURE
		end 

		/* save a copy of @bm */
		declare @origin_bm varbinary(500)
		set  @origin_bm =  @bm

		/* only do the map down when needed */
		set @missingbm = 0x00 
		if update([rowguid])
		begin
			if @@trancount > 0
				rollback tran
	                
			RAISERROR (20062, 16, -1)
		end  
		else    
			set @lineage = { fn UPDATELINEAGE(0x0, @replnick, @oldmaxversion+1) } 
            set @cv = NULL 
            
			set @partgen = NULL 
			update [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] with (rowlock)
			set lineage = { fn UPDATELINEAGE(lineage, @replnick, @oldmaxversion+1) }, 
				generation = @newgen, 
				partchangegen = case when (@partchange = 1 or @joinchange = 1) then @newgen else partchangegen end, 
	            
					colv1 = NULL
			FROM inserted as I JOIN [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] as V with (rowlock)
			ON (I.rowguidcol=V.rowguid)
			and V.tablenick = @tablenick
			option (force order, loop join)

			select @updateerror = @@error, @contents_rows_updated = @@rowcount 
	          
			if @article_rows_updated <> @contents_rows_updated
			begin  
				insert into [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] with (rowlock) (tablenick, rowguid, lineage, colv1, generation, partchangegen) 
				select @tablenick, rowguidcol, @lineage, @cv, @newgen, @partgen
				from inserted i 
				where not exists (select rowguid from [dbo].[MSmerge_ctsv_D0273A169FAA46CDBBF406744E1C214B] with (readcommitted, rowlock, readpast) where tablenick = @tablenick and rowguid = i.rowguidcol)     
				if @@error <> 0
					GOTO FAILURE
			end     

		return
	FAILURE:
		if @@trancount > 0
			rollback tran
		raiserror (20041, 16, -1)
		return 

		
GO
CREATE UNIQUE NONCLUSTERED INDEX [MSmerge_index_882102183] ON [dbo].[Analytics_Service_Time_Averages] ([rowguid]) ON [PRIMARY]
GO
