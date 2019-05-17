CREATE VIEW dbo.QuerylearningActivity
AS
SELECT   TOP (100) PERCENT dbo.LearningActivity.Start AS 开始时间, dbo.LearningActivity.During AS 持续时长, 
                dbo.LearningActivity.WorksUrl AS 作品地址, dbo.LearningActivity.Others AS 活动描述, 
                dbo.ActivityType.Action AS 活动类型, dbo.LearningObject.Title AS 学习对象, 
                dbo.LearningObject.Description AS 对象描述, dbo.LearningObject.LearningContent AS 学习内容, 
                dbo.Location.Address AS 活动地点, dbo.LearningActivity.UserID, dbo.LearningActivity.LearningActivityID
FROM      dbo.LearningActivity INNER JOIN
                dbo.ActivityType ON dbo.LearningActivity.ActivityTypeID = dbo.ActivityType.ActivityTypeID INNER JOIN
                dbo.LearningObject ON dbo.LearningActivity.LearningObjectID = dbo.LearningObject.LearningObjectID INNER JOIN
                dbo.Location ON dbo.LearningActivity.LocationID = dbo.Location.LocationID
WHERE   (dbo.LearningActivity.Flag = 1)
ORDER BY 开始时间 DESC

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[30] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "LearningActivity"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 241
               Right = 246
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ActivityType"
            Begin Extent = 
               Top = 7
               Left = 330
               Bottom = 137
               Right = 500
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LearningObject"
            Begin Extent = 
               Top = 6
               Left = 558
               Bottom = 228
               Right = 748
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Location"
            Begin Extent = 
               Top = 144
               Left = 314
               Bottom = 274
               Right = 469
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 17
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 7395
         Alias = 3555
         Table = 2610
         Output = 720
         Append = ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'QuerylearningActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'QuerylearningActivity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'QuerylearningActivity';

