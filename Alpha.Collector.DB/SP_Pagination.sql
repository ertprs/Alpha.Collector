CREATE DEFINER=`root`@`localhost` PROCEDURE `SP_Pagination`(in tbName varchar(20),#����  
    in returnField varchar(100),#Ҫ��ʾ������  
    in queryStr varchar(500),#where����(ֻ��Ҫдwhere��������)  
    in orderField varchar(500),#����������ֻ��Ҫдorder by�������䣩  
    in pageSize int,#ÿһҳ��ʾ�ļ�¼��  
    in pageIndex int,#��ǰҳ  
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