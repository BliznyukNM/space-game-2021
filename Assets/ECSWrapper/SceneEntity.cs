namespace NB.ECS.Wrapper
{
    public class SceneEntity : MonoEntity
    {
        protected override ISpace space => sceneSpace ?? (sceneSpace = FindObjectOfType<SceneSpace>());
        private SceneSpace sceneSpace;
    }
}
