namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.IO;

    public partial class RunSqlScript : DbMigration
    {
        public override void Up()
        {

            string sqlResName = typeof(RunSqlScript).Namespace + ".202101131418201_RunSqlScript.sql";
            this.SqlResource(sqlResName);

        }
        
        public override void Down()
        {
        }
    }
}
