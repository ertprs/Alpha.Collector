CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_Pagination`(in tbName varchar(20),#表名  
    in returnField varchar(100),#要显示的列名  
    in queryStr varchar(500),#where条件(只需要写where后面的语句)  
    in orderField varchar(500),#排序条件（只需要写order by后面的语句）  
    in pageSize int,#每一页显示的记录数  
    in pageIndex int,#当前页  
    out itemCount int,
    out pageCount int)
BEGIN  
    if (pageSize < 1)then  
      set pageSize = 20;  
    end if;  
      
    if (pageIndex < 1)then  
      set pageIndex = 1;  
    end if;  
      
    if(LENGTH(queryStr)>0)then  
        set queryStr=CONCAT(' where ',queryStr);  
    end if;  
      
    if(LENGTH(orderField)>0)then  
        set orderField = CONCAT(' order by ',orderField);  
    end if;  
      
    set @strsql = CONCAT('select ', returnField, ' from ', tbName, ' ', queryStr, ' ', orderField, ' limit ', pageIndex * pageSize - pageSize, ',', pageSize);  
      
    prepare stmtsql from @strsql;  
    execute stmtsql;  
    deallocate prepare stmtsql;  
      
    set @strsqlcount=concat('select count(1) as count into @itemCount from ',tbName,'',queryStr);  
    prepare stmtsqlcount from @strsqlcount;  
    execute stmtsqlcount;  
    deallocate prepare stmtsqlcount;  
    set itemCount=@itemCount;  
    set pageCount = (itemCount + pageIndex - 1) / pageSize;
END