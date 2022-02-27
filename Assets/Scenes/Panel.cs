public class Panel : UIElement {
    public override void Open() {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public override void Close() {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
