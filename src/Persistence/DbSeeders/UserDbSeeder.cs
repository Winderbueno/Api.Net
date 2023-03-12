using Domain.Entities;
using Domain.Entities.Abstract;
using Domain.Enums;

namespace Persistence
{
    public static class UserDbSeeder
    {
        public static List<T> seedEntity<T>(List<string> names)
        {
            List<T> entities = new List<T> { };

            int i = 1;

            if (entities is List<Role> roles)
                names.ForEach(name =>
                {
                    roles.Add(new Role
                    {
                        RoleId = i++,
                        UserType = UserType.Entreprise,
                        Fonction = Fonction.Support,
                        Name = name
                    });
                });

            if (entities is List<Feature> feats)
                names.ForEach(name =>
                {
                    feats.Add(new Feature
                    {
                        FeatureId = i++,
                        Beta = false,
                        Name = name
                    });
                });

            if (entities is List<Permission> ps)
                names.ForEach(name => { ps.Add(new Permission { PermissionId = i++, Name = name }); });


            // TrackedEntity
            if (entities.First() is TrackedEntity)
            {
                entities.ForEach(entity =>
                {
                    TrackedEntity? trackedEntity = entity as TrackedEntity;
                    trackedEntity!.CreatedAt = DateTime.Now;
                    trackedEntity.CreatedBy = "DbInit";
                    trackedEntity.ModifiedAt = DateTime.Now;
                    trackedEntity.ModifiedBy = "DbInit";
                });
            }

            return entities;
        }

        public static object[] seedRelationFeaturePermission(Dictionary<int, List<int>> relations)
        {
            var rels = (new[] {
                new { 
                    FeaturesFeatureId = relations.Keys.First(), 
                    PermissionsPermissionId = relations[relations.Keys.First()].First() 
                }
            }).ToList();

            var keys = relations.Keys.ToList();

            bool init = true;
            keys.ForEach(key => {
                relations[key].ForEach(relEntity => {
                    if (init) init = false;
                    else rels.Add(new { FeaturesFeatureId = key, PermissionsPermissionId = relEntity });
                });                
            });

            return rels.ToArray();
        }

        public static object[] seedRelationRoleFeature(Dictionary<int, List<int>> relations)
        {
            var rels = (new[] {
                new {
                    RolesRoleId = relations.Keys.First(),
                    FeaturesFeatureId = relations[relations.Keys.First()].First()
                }
            }).ToList();

            var keys = relations.Keys.ToList();

            bool init = true;
            keys.ForEach(key => {
                relations[key].ForEach(relEntity => {
                    if (init) init = false;
                    else rels.Add(new { RolesRoleId = key, FeaturesFeatureId = relEntity });
                });
            });

            return rels.ToArray();
        }
    }
}