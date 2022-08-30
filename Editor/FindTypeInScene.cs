using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
    internal static class FindTypeInScene
    {
        [MenuItem( "CONTEXT/Component/Find Type In Scene" )]
        private static void Find( MenuCommand menuCommand )
        {
            var type = menuCommand.context.GetType();
            SetSearchFilter( $"t:{type.Name}" );
        }

        private enum FilterMode
        {
            ALL,
            NAME,
            TYPE,
        }

        private static void SetSearchFilter( string filter, FilterMode filterMode = FilterMode.ALL )
        {
            var hierarchy = Resources
                    .FindObjectsOfTypeAll<SearchableEditorWindow>()
                    .FirstOrDefault( x => x.GetType().ToString() == "UnityEditor.SceneHierarchyWindow" )
                ;

            if ( hierarchy == null ) return;

            var setSearchType = typeof( SearchableEditorWindow )
                    .GetMethod( "SetSearchFilter", BindingFlags.NonPublic | BindingFlags.Instance )
                ;

            var parameters = new object[] { filter, ( int )filterMode, false, false };

            setSearchType.Invoke( hierarchy, parameters );
        }
    }
}