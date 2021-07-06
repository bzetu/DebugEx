using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEditor.Rendering.Universal
{
    [VolumeComponentEditor(typeof(Grab))]
    sealed class GrabEditor : VolumeComponentEditor
    {
        SerializedDataParameter m_Enable;

        public override void OnEnable()
        {
            var o = new PropertyFetcher<Grab>(serializedObject);

            m_Enable = Unpack(o.Find(x => x.enabled));
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Grab", EditorStyles.miniLabel);
            PropertyField(m_Enable);
        }
    }
}
