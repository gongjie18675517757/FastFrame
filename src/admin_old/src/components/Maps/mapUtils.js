export function removeScript(src) {
  var scripts = document.getElementsByTagName("script");
  for (var i = 0; i < scripts.length; i++) {
    if (scripts[i] && scripts[i].src && scripts[i].src.indexOf(src) != -1) {
      scripts[i].parentNode.removeChild(scripts[i]);
    }
  }
}

export function loadScript(src, cb) {
  var addSign = true;
  var scripts = document.getElementsByTagName("script");
  for (var i = 0; i < scripts.length; i++) {
    if (scripts[i] && scripts[i].src && scripts[i].src.indexOf(src) != -1) {
      addSign = false;
    }
  }
  if (addSign) {
    let cbName = `cb_${new Date().getTime()}`;
    window[cbName] = function () {
      delete window[cbName];
      cb();
    };
    src = `${src}&callback=${cbName}`;
    var $script = document.createElement("script");
    $script.setAttribute("type", "text/javascript");
    $script.setAttribute("src", src);
    $script.setAttribute("async", '');
    $script.setAttribute("defer", '');
    document
      .getElementsByTagName("head")
      .item(0)
      .appendChild($script);
  } else {
    cb();
  }
}

/**
 * 地图组件基类
 */
export const Base = {
  inject: ["getMap"],
  data() {
    return {
      instance: null
    }
  },
  computed: {
    map() {
      return this.getMap();
    }
  },
  created() {
    if (this.map) {
      this.load();
    }
  },
  watch: {
    map(val) {
      if (val) {
        this.load();
      }
    }
  },
  methods: {
    load() {}
  }
}