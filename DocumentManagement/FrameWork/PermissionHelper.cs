//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DocumentManagement.FrameWork
//{
//    public class PermissionHelper
//    {
//        public static bool HasEffectivePermission(int userK, string permissionName, string groupingName)
//        {
//            //GetEffectivePermissions(userK).Count(i => i.Name == permissionName && i.GroupingName == groupingName) > 0
//            return true;
//        }

//        // For a single API function which is responsible for more than two permissions, in which we need to check the one of the permission in the Group 
//        // createOrUpdate() need to authorize any one of the two Add and Edit Permission.
//        // example: NoteController. CreateOrUpdate. 

//        public static bool HasOneOfTheEffectivePermissionsInGroup(int userK, List<string> permissionNames, string groupingName)
//        {
//            return true;
//            //return GetEffectivePermissions(userK).Count(i => permissionNames.Contains(i.Name) && i.GroupingName == groupingName) > 0;
//        }

//        //public static List<Permission> GetEffectivePermissions(int userK)
//        //{
//        //    var userRepo = new UserService(new CoreDB());
//        //    var userGroupPermissions = userRepo.FindUserGroupsPermissions(userK);
//        //    var userPermissions = userRepo.FindUserPermissions(userK);

//        //    var totalPermissions = new List<Permission>();
//        //    totalPermissions.AddRange(userPermissions);
//        //    totalPermissions.AddRange(userGroupPermissions);

//        //    totalPermissions = totalPermissions.Distinct().ToList();

//        //    return totalPermissions;
//        //}
//    }
//}
