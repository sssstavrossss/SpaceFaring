namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RunSqlScript2 : DbMigration
    {
        public override void Up()
        {
            string sqlResName = typeof(RunSqlScript).Namespace + ".202101131555396_RunSqlScript2.sql";
            this.SqlResource(sqlResName);
        }
        
        public override void Down()
        {
        }
    }
}
