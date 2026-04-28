import {
  require_checkPropTypes,
  require_object_assign
} from "./chunk-HL67TA5W.js";
import {
  __commonJS
} from "./chunk-LK32TJAX.js";

// node_modules/@react-latest-ui/react-sticky-notes/node_modules/react/cjs/react.development.js
var require_react_development = __commonJS({
  "node_modules/@react-latest-ui/react-sticky-notes/node_modules/react/cjs/react.development.js"(exports) {
    "use strict";
    if (true) {
      (function() {
        "use strict";
        var _assign = require_object_assign();
        var checkPropTypes = require_checkPropTypes();
        var ReactVersion = "16.14.0";
        var hasSymbol = typeof Symbol === "function" && Symbol.for;
        var REACT_ELEMENT_TYPE = hasSymbol ? Symbol.for("react.element") : 60103;
        var REACT_PORTAL_TYPE = hasSymbol ? Symbol.for("react.portal") : 60106;
        var REACT_FRAGMENT_TYPE = hasSymbol ? Symbol.for("react.fragment") : 60107;
        var REACT_STRICT_MODE_TYPE = hasSymbol ? Symbol.for("react.strict_mode") : 60108;
        var REACT_PROFILER_TYPE = hasSymbol ? Symbol.for("react.profiler") : 60114;
        var REACT_PROVIDER_TYPE = hasSymbol ? Symbol.for("react.provider") : 60109;
        var REACT_CONTEXT_TYPE = hasSymbol ? Symbol.for("react.context") : 60110;
        var REACT_CONCURRENT_MODE_TYPE = hasSymbol ? Symbol.for("react.concurrent_mode") : 60111;
        var REACT_FORWARD_REF_TYPE = hasSymbol ? Symbol.for("react.forward_ref") : 60112;
        var REACT_SUSPENSE_TYPE = hasSymbol ? Symbol.for("react.suspense") : 60113;
        var REACT_SUSPENSE_LIST_TYPE = hasSymbol ? Symbol.for("react.suspense_list") : 60120;
        var REACT_MEMO_TYPE = hasSymbol ? Symbol.for("react.memo") : 60115;
        var REACT_LAZY_TYPE = hasSymbol ? Symbol.for("react.lazy") : 60116;
        var REACT_BLOCK_TYPE = hasSymbol ? Symbol.for("react.block") : 60121;
        var REACT_FUNDAMENTAL_TYPE = hasSymbol ? Symbol.for("react.fundamental") : 60117;
        var REACT_RESPONDER_TYPE = hasSymbol ? Symbol.for("react.responder") : 60118;
        var REACT_SCOPE_TYPE = hasSymbol ? Symbol.for("react.scope") : 60119;
        var MAYBE_ITERATOR_SYMBOL = typeof Symbol === "function" && Symbol.iterator;
        var FAUX_ITERATOR_SYMBOL = "@@iterator";
        function getIteratorFn(maybeIterable) {
          if (maybeIterable === null || typeof maybeIterable !== "object") {
            return null;
          }
          var maybeIterator = MAYBE_ITERATOR_SYMBOL && maybeIterable[MAYBE_ITERATOR_SYMBOL] || maybeIterable[FAUX_ITERATOR_SYMBOL];
          if (typeof maybeIterator === "function") {
            return maybeIterator;
          }
          return null;
        }
        var ReactCurrentDispatcher = {
          /**
           * @internal
           * @type {ReactComponent}
           */
          current: null
        };
        var ReactCurrentBatchConfig = {
          suspense: null
        };
        var ReactCurrentOwner = {
          /**
           * @internal
           * @type {ReactComponent}
           */
          current: null
        };
        var BEFORE_SLASH_RE = /^(.*)[\\\/]/;
        function describeComponentFrame(name, source, ownerName) {
          var sourceInfo = "";
          if (source) {
            var path = source.fileName;
            var fileName = path.replace(BEFORE_SLASH_RE, "");
            {
              if (/^index\./.test(fileName)) {
                var match = path.match(BEFORE_SLASH_RE);
                if (match) {
                  var pathBeforeSlash = match[1];
                  if (pathBeforeSlash) {
                    var folderName = pathBeforeSlash.replace(BEFORE_SLASH_RE, "");
                    fileName = folderName + "/" + fileName;
                  }
                }
              }
            }
            sourceInfo = " (at " + fileName + ":" + source.lineNumber + ")";
          } else if (ownerName) {
            sourceInfo = " (created by " + ownerName + ")";
          }
          return "\n    in " + (name || "Unknown") + sourceInfo;
        }
        var Resolved = 1;
        function refineResolvedLazyComponent(lazyComponent) {
          return lazyComponent._status === Resolved ? lazyComponent._result : null;
        }
        function getWrappedName(outerType, innerType, wrapperName) {
          var functionName = innerType.displayName || innerType.name || "";
          return outerType.displayName || (functionName !== "" ? wrapperName + "(" + functionName + ")" : wrapperName);
        }
        function getComponentName(type) {
          if (type == null) {
            return null;
          }
          {
            if (typeof type.tag === "number") {
              error("Received an unexpected object in getComponentName(). This is likely a bug in React. Please file an issue.");
            }
          }
          if (typeof type === "function") {
            return type.displayName || type.name || null;
          }
          if (typeof type === "string") {
            return type;
          }
          switch (type) {
            case REACT_FRAGMENT_TYPE:
              return "Fragment";
            case REACT_PORTAL_TYPE:
              return "Portal";
            case REACT_PROFILER_TYPE:
              return "Profiler";
            case REACT_STRICT_MODE_TYPE:
              return "StrictMode";
            case REACT_SUSPENSE_TYPE:
              return "Suspense";
            case REACT_SUSPENSE_LIST_TYPE:
              return "SuspenseList";
          }
          if (typeof type === "object") {
            switch (type.$$typeof) {
              case REACT_CONTEXT_TYPE:
                return "Context.Consumer";
              case REACT_PROVIDER_TYPE:
                return "Context.Provider";
              case REACT_FORWARD_REF_TYPE:
                return getWrappedName(type, type.render, "ForwardRef");
              case REACT_MEMO_TYPE:
                return getComponentName(type.type);
              case REACT_BLOCK_TYPE:
                return getComponentName(type.render);
              case REACT_LAZY_TYPE: {
                var thenable = type;
                var resolvedThenable = refineResolvedLazyComponent(thenable);
                if (resolvedThenable) {
                  return getComponentName(resolvedThenable);
                }
                break;
              }
            }
          }
          return null;
        }
        var ReactDebugCurrentFrame = {};
        var currentlyValidatingElement = null;
        function setCurrentlyValidatingElement(element) {
          {
            currentlyValidatingElement = element;
          }
        }
        {
          ReactDebugCurrentFrame.getCurrentStack = null;
          ReactDebugCurrentFrame.getStackAddendum = function() {
            var stack = "";
            if (currentlyValidatingElement) {
              var name = getComponentName(currentlyValidatingElement.type);
              var owner = currentlyValidatingElement._owner;
              stack += describeComponentFrame(name, currentlyValidatingElement._source, owner && getComponentName(owner.type));
            }
            var impl = ReactDebugCurrentFrame.getCurrentStack;
            if (impl) {
              stack += impl() || "";
            }
            return stack;
          };
        }
        var IsSomeRendererActing = {
          current: false
        };
        var ReactSharedInternals = {
          ReactCurrentDispatcher,
          ReactCurrentBatchConfig,
          ReactCurrentOwner,
          IsSomeRendererActing,
          // Used by renderers to avoid bundling object-assign twice in UMD bundles:
          assign: _assign
        };
        {
          _assign(ReactSharedInternals, {
            // These should not be included in production.
            ReactDebugCurrentFrame,
            // Shim for React DOM 16.0.0 which still destructured (but not used) this.
            // TODO: remove in React 17.0.
            ReactComponentTreeHook: {}
          });
        }
        function warn(format) {
          {
            for (var _len = arguments.length, args = new Array(_len > 1 ? _len - 1 : 0), _key = 1; _key < _len; _key++) {
              args[_key - 1] = arguments[_key];
            }
            printWarning("warn", format, args);
          }
        }
        function error(format) {
          {
            for (var _len2 = arguments.length, args = new Array(_len2 > 1 ? _len2 - 1 : 0), _key2 = 1; _key2 < _len2; _key2++) {
              args[_key2 - 1] = arguments[_key2];
            }
            printWarning("error", format, args);
          }
        }
        function printWarning(level, format, args) {
          {
            var hasExistingStack = args.length > 0 && typeof args[args.length - 1] === "string" && args[args.length - 1].indexOf("\n    in") === 0;
            if (!hasExistingStack) {
              var ReactDebugCurrentFrame2 = ReactSharedInternals.ReactDebugCurrentFrame;
              var stack = ReactDebugCurrentFrame2.getStackAddendum();
              if (stack !== "") {
                format += "%s";
                args = args.concat([stack]);
              }
            }
            var argsWithFormat = args.map(function(item) {
              return "" + item;
            });
            argsWithFormat.unshift("Warning: " + format);
            Function.prototype.apply.call(console[level], console, argsWithFormat);
            try {
              var argIndex = 0;
              var message = "Warning: " + format.replace(/%s/g, function() {
                return args[argIndex++];
              });
              throw new Error(message);
            } catch (x) {
            }
          }
        }
        var didWarnStateUpdateForUnmountedComponent = {};
        function warnNoop(publicInstance, callerName) {
          {
            var _constructor = publicInstance.constructor;
            var componentName = _constructor && (_constructor.displayName || _constructor.name) || "ReactClass";
            var warningKey = componentName + "." + callerName;
            if (didWarnStateUpdateForUnmountedComponent[warningKey]) {
              return;
            }
            error("Can't call %s on a component that is not yet mounted. This is a no-op, but it might indicate a bug in your application. Instead, assign to `this.state` directly or define a `state = {};` class property with the desired state in the %s component.", callerName, componentName);
            didWarnStateUpdateForUnmountedComponent[warningKey] = true;
          }
        }
        var ReactNoopUpdateQueue = {
          /**
           * Checks whether or not this composite component is mounted.
           * @param {ReactClass} publicInstance The instance we want to test.
           * @return {boolean} True if mounted, false otherwise.
           * @protected
           * @final
           */
          isMounted: function(publicInstance) {
            return false;
          },
          /**
           * Forces an update. This should only be invoked when it is known with
           * certainty that we are **not** in a DOM transaction.
           *
           * You may want to call this when you know that some deeper aspect of the
           * component's state has changed but `setState` was not called.
           *
           * This will not invoke `shouldComponentUpdate`, but it will invoke
           * `componentWillUpdate` and `componentDidUpdate`.
           *
           * @param {ReactClass} publicInstance The instance that should rerender.
           * @param {?function} callback Called after component is updated.
           * @param {?string} callerName name of the calling function in the public API.
           * @internal
           */
          enqueueForceUpdate: function(publicInstance, callback, callerName) {
            warnNoop(publicInstance, "forceUpdate");
          },
          /**
           * Replaces all of the state. Always use this or `setState` to mutate state.
           * You should treat `this.state` as immutable.
           *
           * There is no guarantee that `this.state` will be immediately updated, so
           * accessing `this.state` after calling this method may return the old value.
           *
           * @param {ReactClass} publicInstance The instance that should rerender.
           * @param {object} completeState Next state.
           * @param {?function} callback Called after component is updated.
           * @param {?string} callerName name of the calling function in the public API.
           * @internal
           */
          enqueueReplaceState: function(publicInstance, completeState, callback, callerName) {
            warnNoop(publicInstance, "replaceState");
          },
          /**
           * Sets a subset of the state. This only exists because _pendingState is
           * internal. This provides a merging strategy that is not available to deep
           * properties which is confusing. TODO: Expose pendingState or don't use it
           * during the merge.
           *
           * @param {ReactClass} publicInstance The instance that should rerender.
           * @param {object} partialState Next partial state to be merged with state.
           * @param {?function} callback Called after component is updated.
           * @param {?string} Name of the calling function in the public API.
           * @internal
           */
          enqueueSetState: function(publicInstance, partialState, callback, callerName) {
            warnNoop(publicInstance, "setState");
          }
        };
        var emptyObject = {};
        {
          Object.freeze(emptyObject);
        }
        function Component(props, context, updater) {
          this.props = props;
          this.context = context;
          this.refs = emptyObject;
          this.updater = updater || ReactNoopUpdateQueue;
        }
        Component.prototype.isReactComponent = {};
        Component.prototype.setState = function(partialState, callback) {
          if (!(typeof partialState === "object" || typeof partialState === "function" || partialState == null)) {
            {
              throw Error("setState(...): takes an object of state variables to update or a function which returns an object of state variables.");
            }
          }
          this.updater.enqueueSetState(this, partialState, callback, "setState");
        };
        Component.prototype.forceUpdate = function(callback) {
          this.updater.enqueueForceUpdate(this, callback, "forceUpdate");
        };
        {
          var deprecatedAPIs = {
            isMounted: ["isMounted", "Instead, make sure to clean up subscriptions and pending requests in componentWillUnmount to prevent memory leaks."],
            replaceState: ["replaceState", "Refactor your code to use setState instead (see https://github.com/facebook/react/issues/3236)."]
          };
          var defineDeprecationWarning = function(methodName, info) {
            Object.defineProperty(Component.prototype, methodName, {
              get: function() {
                warn("%s(...) is deprecated in plain JavaScript React classes. %s", info[0], info[1]);
                return void 0;
              }
            });
          };
          for (var fnName in deprecatedAPIs) {
            if (deprecatedAPIs.hasOwnProperty(fnName)) {
              defineDeprecationWarning(fnName, deprecatedAPIs[fnName]);
            }
          }
        }
        function ComponentDummy() {
        }
        ComponentDummy.prototype = Component.prototype;
        function PureComponent(props, context, updater) {
          this.props = props;
          this.context = context;
          this.refs = emptyObject;
          this.updater = updater || ReactNoopUpdateQueue;
        }
        var pureComponentPrototype = PureComponent.prototype = new ComponentDummy();
        pureComponentPrototype.constructor = PureComponent;
        _assign(pureComponentPrototype, Component.prototype);
        pureComponentPrototype.isPureReactComponent = true;
        function createRef() {
          var refObject = {
            current: null
          };
          {
            Object.seal(refObject);
          }
          return refObject;
        }
        var hasOwnProperty = Object.prototype.hasOwnProperty;
        var RESERVED_PROPS = {
          key: true,
          ref: true,
          __self: true,
          __source: true
        };
        var specialPropKeyWarningShown, specialPropRefWarningShown, didWarnAboutStringRefs;
        {
          didWarnAboutStringRefs = {};
        }
        function hasValidRef(config) {
          {
            if (hasOwnProperty.call(config, "ref")) {
              var getter = Object.getOwnPropertyDescriptor(config, "ref").get;
              if (getter && getter.isReactWarning) {
                return false;
              }
            }
          }
          return config.ref !== void 0;
        }
        function hasValidKey(config) {
          {
            if (hasOwnProperty.call(config, "key")) {
              var getter = Object.getOwnPropertyDescriptor(config, "key").get;
              if (getter && getter.isReactWarning) {
                return false;
              }
            }
          }
          return config.key !== void 0;
        }
        function defineKeyPropWarningGetter(props, displayName) {
          var warnAboutAccessingKey = function() {
            {
              if (!specialPropKeyWarningShown) {
                specialPropKeyWarningShown = true;
                error("%s: `key` is not a prop. Trying to access it will result in `undefined` being returned. If you need to access the same value within the child component, you should pass it as a different prop. (https://fb.me/react-special-props)", displayName);
              }
            }
          };
          warnAboutAccessingKey.isReactWarning = true;
          Object.defineProperty(props, "key", {
            get: warnAboutAccessingKey,
            configurable: true
          });
        }
        function defineRefPropWarningGetter(props, displayName) {
          var warnAboutAccessingRef = function() {
            {
              if (!specialPropRefWarningShown) {
                specialPropRefWarningShown = true;
                error("%s: `ref` is not a prop. Trying to access it will result in `undefined` being returned. If you need to access the same value within the child component, you should pass it as a different prop. (https://fb.me/react-special-props)", displayName);
              }
            }
          };
          warnAboutAccessingRef.isReactWarning = true;
          Object.defineProperty(props, "ref", {
            get: warnAboutAccessingRef,
            configurable: true
          });
        }
        function warnIfStringRefCannotBeAutoConverted(config) {
          {
            if (typeof config.ref === "string" && ReactCurrentOwner.current && config.__self && ReactCurrentOwner.current.stateNode !== config.__self) {
              var componentName = getComponentName(ReactCurrentOwner.current.type);
              if (!didWarnAboutStringRefs[componentName]) {
                error('Component "%s" contains the string ref "%s". Support for string refs will be removed in a future major release. This case cannot be automatically converted to an arrow function. We ask you to manually fix this case by using useRef() or createRef() instead. Learn more about using refs safely here: https://fb.me/react-strict-mode-string-ref', getComponentName(ReactCurrentOwner.current.type), config.ref);
                didWarnAboutStringRefs[componentName] = true;
              }
            }
          }
        }
        var ReactElement = function(type, key, ref, self, source, owner, props) {
          var element = {
            // This tag allows us to uniquely identify this as a React Element
            $$typeof: REACT_ELEMENT_TYPE,
            // Built-in properties that belong on the element
            type,
            key,
            ref,
            props,
            // Record the component responsible for creating this element.
            _owner: owner
          };
          {
            element._store = {};
            Object.defineProperty(element._store, "validated", {
              configurable: false,
              enumerable: false,
              writable: true,
              value: false
            });
            Object.defineProperty(element, "_self", {
              configurable: false,
              enumerable: false,
              writable: false,
              value: self
            });
            Object.defineProperty(element, "_source", {
              configurable: false,
              enumerable: false,
              writable: false,
              value: source
            });
            if (Object.freeze) {
              Object.freeze(element.props);
              Object.freeze(element);
            }
          }
          return element;
        };
        function createElement(type, config, children) {
          var propName;
          var props = {};
          var key = null;
          var ref = null;
          var self = null;
          var source = null;
          if (config != null) {
            if (hasValidRef(config)) {
              ref = config.ref;
              {
                warnIfStringRefCannotBeAutoConverted(config);
              }
            }
            if (hasValidKey(config)) {
              key = "" + config.key;
            }
            self = config.__self === void 0 ? null : config.__self;
            source = config.__source === void 0 ? null : config.__source;
            for (propName in config) {
              if (hasOwnProperty.call(config, propName) && !RESERVED_PROPS.hasOwnProperty(propName)) {
                props[propName] = config[propName];
              }
            }
          }
          var childrenLength = arguments.length - 2;
          if (childrenLength === 1) {
            props.children = children;
          } else if (childrenLength > 1) {
            var childArray = Array(childrenLength);
            for (var i = 0; i < childrenLength; i++) {
              childArray[i] = arguments[i + 2];
            }
            {
              if (Object.freeze) {
                Object.freeze(childArray);
              }
            }
            props.children = childArray;
          }
          if (type && type.defaultProps) {
            var defaultProps = type.defaultProps;
            for (propName in defaultProps) {
              if (props[propName] === void 0) {
                props[propName] = defaultProps[propName];
              }
            }
          }
          {
            if (key || ref) {
              var displayName = typeof type === "function" ? type.displayName || type.name || "Unknown" : type;
              if (key) {
                defineKeyPropWarningGetter(props, displayName);
              }
              if (ref) {
                defineRefPropWarningGetter(props, displayName);
              }
            }
          }
          return ReactElement(type, key, ref, self, source, ReactCurrentOwner.current, props);
        }
        function cloneAndReplaceKey(oldElement, newKey) {
          var newElement = ReactElement(oldElement.type, newKey, oldElement.ref, oldElement._self, oldElement._source, oldElement._owner, oldElement.props);
          return newElement;
        }
        function cloneElement(element, config, children) {
          if (!!(element === null || element === void 0)) {
            {
              throw Error("React.cloneElement(...): The argument must be a React element, but you passed " + element + ".");
            }
          }
          var propName;
          var props = _assign({}, element.props);
          var key = element.key;
          var ref = element.ref;
          var self = element._self;
          var source = element._source;
          var owner = element._owner;
          if (config != null) {
            if (hasValidRef(config)) {
              ref = config.ref;
              owner = ReactCurrentOwner.current;
            }
            if (hasValidKey(config)) {
              key = "" + config.key;
            }
            var defaultProps;
            if (element.type && element.type.defaultProps) {
              defaultProps = element.type.defaultProps;
            }
            for (propName in config) {
              if (hasOwnProperty.call(config, propName) && !RESERVED_PROPS.hasOwnProperty(propName)) {
                if (config[propName] === void 0 && defaultProps !== void 0) {
                  props[propName] = defaultProps[propName];
                } else {
                  props[propName] = config[propName];
                }
              }
            }
          }
          var childrenLength = arguments.length - 2;
          if (childrenLength === 1) {
            props.children = children;
          } else if (childrenLength > 1) {
            var childArray = Array(childrenLength);
            for (var i = 0; i < childrenLength; i++) {
              childArray[i] = arguments[i + 2];
            }
            props.children = childArray;
          }
          return ReactElement(element.type, key, ref, self, source, owner, props);
        }
        function isValidElement(object) {
          return typeof object === "object" && object !== null && object.$$typeof === REACT_ELEMENT_TYPE;
        }
        var SEPARATOR = ".";
        var SUBSEPARATOR = ":";
        function escape(key) {
          var escapeRegex = /[=:]/g;
          var escaperLookup = {
            "=": "=0",
            ":": "=2"
          };
          var escapedString = ("" + key).replace(escapeRegex, function(match) {
            return escaperLookup[match];
          });
          return "$" + escapedString;
        }
        var didWarnAboutMaps = false;
        var userProvidedKeyEscapeRegex = /\/+/g;
        function escapeUserProvidedKey(text) {
          return ("" + text).replace(userProvidedKeyEscapeRegex, "$&/");
        }
        var POOL_SIZE = 10;
        var traverseContextPool = [];
        function getPooledTraverseContext(mapResult, keyPrefix, mapFunction, mapContext) {
          if (traverseContextPool.length) {
            var traverseContext = traverseContextPool.pop();
            traverseContext.result = mapResult;
            traverseContext.keyPrefix = keyPrefix;
            traverseContext.func = mapFunction;
            traverseContext.context = mapContext;
            traverseContext.count = 0;
            return traverseContext;
          } else {
            return {
              result: mapResult,
              keyPrefix,
              func: mapFunction,
              context: mapContext,
              count: 0
            };
          }
        }
        function releaseTraverseContext(traverseContext) {
          traverseContext.result = null;
          traverseContext.keyPrefix = null;
          traverseContext.func = null;
          traverseContext.context = null;
          traverseContext.count = 0;
          if (traverseContextPool.length < POOL_SIZE) {
            traverseContextPool.push(traverseContext);
          }
        }
        function traverseAllChildrenImpl(children, nameSoFar, callback, traverseContext) {
          var type = typeof children;
          if (type === "undefined" || type === "boolean") {
            children = null;
          }
          var invokeCallback = false;
          if (children === null) {
            invokeCallback = true;
          } else {
            switch (type) {
              case "string":
              case "number":
                invokeCallback = true;
                break;
              case "object":
                switch (children.$$typeof) {
                  case REACT_ELEMENT_TYPE:
                  case REACT_PORTAL_TYPE:
                    invokeCallback = true;
                }
            }
          }
          if (invokeCallback) {
            callback(
              traverseContext,
              children,
              // If it's the only child, treat the name as if it was wrapped in an array
              // so that it's consistent if the number of children grows.
              nameSoFar === "" ? SEPARATOR + getComponentKey(children, 0) : nameSoFar
            );
            return 1;
          }
          var child;
          var nextName;
          var subtreeCount = 0;
          var nextNamePrefix = nameSoFar === "" ? SEPARATOR : nameSoFar + SUBSEPARATOR;
          if (Array.isArray(children)) {
            for (var i = 0; i < children.length; i++) {
              child = children[i];
              nextName = nextNamePrefix + getComponentKey(child, i);
              subtreeCount += traverseAllChildrenImpl(child, nextName, callback, traverseContext);
            }
          } else {
            var iteratorFn = getIteratorFn(children);
            if (typeof iteratorFn === "function") {
              {
                if (iteratorFn === children.entries) {
                  if (!didWarnAboutMaps) {
                    warn("Using Maps as children is deprecated and will be removed in a future major release. Consider converting children to an array of keyed ReactElements instead.");
                  }
                  didWarnAboutMaps = true;
                }
              }
              var iterator = iteratorFn.call(children);
              var step;
              var ii = 0;
              while (!(step = iterator.next()).done) {
                child = step.value;
                nextName = nextNamePrefix + getComponentKey(child, ii++);
                subtreeCount += traverseAllChildrenImpl(child, nextName, callback, traverseContext);
              }
            } else if (type === "object") {
              var addendum = "";
              {
                addendum = " If you meant to render a collection of children, use an array instead." + ReactDebugCurrentFrame.getStackAddendum();
              }
              var childrenString = "" + children;
              {
                {
                  throw Error("Objects are not valid as a React child (found: " + (childrenString === "[object Object]" ? "object with keys {" + Object.keys(children).join(", ") + "}" : childrenString) + ")." + addendum);
                }
              }
            }
          }
          return subtreeCount;
        }
        function traverseAllChildren(children, callback, traverseContext) {
          if (children == null) {
            return 0;
          }
          return traverseAllChildrenImpl(children, "", callback, traverseContext);
        }
        function getComponentKey(component, index) {
          if (typeof component === "object" && component !== null && component.key != null) {
            return escape(component.key);
          }
          return index.toString(36);
        }
        function forEachSingleChild(bookKeeping, child, name) {
          var func = bookKeeping.func, context = bookKeeping.context;
          func.call(context, child, bookKeeping.count++);
        }
        function forEachChildren(children, forEachFunc, forEachContext) {
          if (children == null) {
            return children;
          }
          var traverseContext = getPooledTraverseContext(null, null, forEachFunc, forEachContext);
          traverseAllChildren(children, forEachSingleChild, traverseContext);
          releaseTraverseContext(traverseContext);
        }
        function mapSingleChildIntoContext(bookKeeping, child, childKey) {
          var result = bookKeeping.result, keyPrefix = bookKeeping.keyPrefix, func = bookKeeping.func, context = bookKeeping.context;
          var mappedChild = func.call(context, child, bookKeeping.count++);
          if (Array.isArray(mappedChild)) {
            mapIntoWithKeyPrefixInternal(mappedChild, result, childKey, function(c) {
              return c;
            });
          } else if (mappedChild != null) {
            if (isValidElement(mappedChild)) {
              mappedChild = cloneAndReplaceKey(
                mappedChild,
                // Keep both the (mapped) and old keys if they differ, just as
                // traverseAllChildren used to do for objects as children
                keyPrefix + (mappedChild.key && (!child || child.key !== mappedChild.key) ? escapeUserProvidedKey(mappedChild.key) + "/" : "") + childKey
              );
            }
            result.push(mappedChild);
          }
        }
        function mapIntoWithKeyPrefixInternal(children, array, prefix, func, context) {
          var escapedPrefix = "";
          if (prefix != null) {
            escapedPrefix = escapeUserProvidedKey(prefix) + "/";
          }
          var traverseContext = getPooledTraverseContext(array, escapedPrefix, func, context);
          traverseAllChildren(children, mapSingleChildIntoContext, traverseContext);
          releaseTraverseContext(traverseContext);
        }
        function mapChildren(children, func, context) {
          if (children == null) {
            return children;
          }
          var result = [];
          mapIntoWithKeyPrefixInternal(children, result, null, func, context);
          return result;
        }
        function countChildren(children) {
          return traverseAllChildren(children, function() {
            return null;
          }, null);
        }
        function toArray(children) {
          var result = [];
          mapIntoWithKeyPrefixInternal(children, result, null, function(child) {
            return child;
          });
          return result;
        }
        function onlyChild(children) {
          if (!isValidElement(children)) {
            {
              throw Error("React.Children.only expected to receive a single React element child.");
            }
          }
          return children;
        }
        function createContext(defaultValue, calculateChangedBits) {
          if (calculateChangedBits === void 0) {
            calculateChangedBits = null;
          } else {
            {
              if (calculateChangedBits !== null && typeof calculateChangedBits !== "function") {
                error("createContext: Expected the optional second argument to be a function. Instead received: %s", calculateChangedBits);
              }
            }
          }
          var context = {
            $$typeof: REACT_CONTEXT_TYPE,
            _calculateChangedBits: calculateChangedBits,
            // As a workaround to support multiple concurrent renderers, we categorize
            // some renderers as primary and others as secondary. We only expect
            // there to be two concurrent renderers at most: React Native (primary) and
            // Fabric (secondary); React DOM (primary) and React ART (secondary).
            // Secondary renderers store their context values on separate fields.
            _currentValue: defaultValue,
            _currentValue2: defaultValue,
            // Used to track how many concurrent renderers this context currently
            // supports within in a single renderer. Such as parallel server rendering.
            _threadCount: 0,
            // These are circular
            Provider: null,
            Consumer: null
          };
          context.Provider = {
            $$typeof: REACT_PROVIDER_TYPE,
            _context: context
          };
          var hasWarnedAboutUsingNestedContextConsumers = false;
          var hasWarnedAboutUsingConsumerProvider = false;
          {
            var Consumer = {
              $$typeof: REACT_CONTEXT_TYPE,
              _context: context,
              _calculateChangedBits: context._calculateChangedBits
            };
            Object.defineProperties(Consumer, {
              Provider: {
                get: function() {
                  if (!hasWarnedAboutUsingConsumerProvider) {
                    hasWarnedAboutUsingConsumerProvider = true;
                    error("Rendering <Context.Consumer.Provider> is not supported and will be removed in a future major release. Did you mean to render <Context.Provider> instead?");
                  }
                  return context.Provider;
                },
                set: function(_Provider) {
                  context.Provider = _Provider;
                }
              },
              _currentValue: {
                get: function() {
                  return context._currentValue;
                },
                set: function(_currentValue) {
                  context._currentValue = _currentValue;
                }
              },
              _currentValue2: {
                get: function() {
                  return context._currentValue2;
                },
                set: function(_currentValue2) {
                  context._currentValue2 = _currentValue2;
                }
              },
              _threadCount: {
                get: function() {
                  return context._threadCount;
                },
                set: function(_threadCount) {
                  context._threadCount = _threadCount;
                }
              },
              Consumer: {
                get: function() {
                  if (!hasWarnedAboutUsingNestedContextConsumers) {
                    hasWarnedAboutUsingNestedContextConsumers = true;
                    error("Rendering <Context.Consumer.Consumer> is not supported and will be removed in a future major release. Did you mean to render <Context.Consumer> instead?");
                  }
                  return context.Consumer;
                }
              }
            });
            context.Consumer = Consumer;
          }
          {
            context._currentRenderer = null;
            context._currentRenderer2 = null;
          }
          return context;
        }
        function lazy(ctor) {
          var lazyType = {
            $$typeof: REACT_LAZY_TYPE,
            _ctor: ctor,
            // React uses these fields to store the result.
            _status: -1,
            _result: null
          };
          {
            var defaultProps;
            var propTypes;
            Object.defineProperties(lazyType, {
              defaultProps: {
                configurable: true,
                get: function() {
                  return defaultProps;
                },
                set: function(newDefaultProps) {
                  error("React.lazy(...): It is not supported to assign `defaultProps` to a lazy component import. Either specify them where the component is defined, or create a wrapping component around it.");
                  defaultProps = newDefaultProps;
                  Object.defineProperty(lazyType, "defaultProps", {
                    enumerable: true
                  });
                }
              },
              propTypes: {
                configurable: true,
                get: function() {
                  return propTypes;
                },
                set: function(newPropTypes) {
                  error("React.lazy(...): It is not supported to assign `propTypes` to a lazy component import. Either specify them where the component is defined, or create a wrapping component around it.");
                  propTypes = newPropTypes;
                  Object.defineProperty(lazyType, "propTypes", {
                    enumerable: true
                  });
                }
              }
            });
          }
          return lazyType;
        }
        function forwardRef(render) {
          {
            if (render != null && render.$$typeof === REACT_MEMO_TYPE) {
              error("forwardRef requires a render function but received a `memo` component. Instead of forwardRef(memo(...)), use memo(forwardRef(...)).");
            } else if (typeof render !== "function") {
              error("forwardRef requires a render function but was given %s.", render === null ? "null" : typeof render);
            } else {
              if (render.length !== 0 && render.length !== 2) {
                error("forwardRef render functions accept exactly two parameters: props and ref. %s", render.length === 1 ? "Did you forget to use the ref parameter?" : "Any additional parameter will be undefined.");
              }
            }
            if (render != null) {
              if (render.defaultProps != null || render.propTypes != null) {
                error("forwardRef render functions do not support propTypes or defaultProps. Did you accidentally pass a React component?");
              }
            }
          }
          return {
            $$typeof: REACT_FORWARD_REF_TYPE,
            render
          };
        }
        function isValidElementType(type) {
          return typeof type === "string" || typeof type === "function" || // Note: its typeof might be other than 'symbol' or 'number' if it's a polyfill.
          type === REACT_FRAGMENT_TYPE || type === REACT_CONCURRENT_MODE_TYPE || type === REACT_PROFILER_TYPE || type === REACT_STRICT_MODE_TYPE || type === REACT_SUSPENSE_TYPE || type === REACT_SUSPENSE_LIST_TYPE || typeof type === "object" && type !== null && (type.$$typeof === REACT_LAZY_TYPE || type.$$typeof === REACT_MEMO_TYPE || type.$$typeof === REACT_PROVIDER_TYPE || type.$$typeof === REACT_CONTEXT_TYPE || type.$$typeof === REACT_FORWARD_REF_TYPE || type.$$typeof === REACT_FUNDAMENTAL_TYPE || type.$$typeof === REACT_RESPONDER_TYPE || type.$$typeof === REACT_SCOPE_TYPE || type.$$typeof === REACT_BLOCK_TYPE);
        }
        function memo(type, compare) {
          {
            if (!isValidElementType(type)) {
              error("memo: The first argument must be a component. Instead received: %s", type === null ? "null" : typeof type);
            }
          }
          return {
            $$typeof: REACT_MEMO_TYPE,
            type,
            compare: compare === void 0 ? null : compare
          };
        }
        function resolveDispatcher() {
          var dispatcher = ReactCurrentDispatcher.current;
          if (!(dispatcher !== null)) {
            {
              throw Error("Invalid hook call. Hooks can only be called inside of the body of a function component. This could happen for one of the following reasons:\n1. You might have mismatching versions of React and the renderer (such as React DOM)\n2. You might be breaking the Rules of Hooks\n3. You might have more than one copy of React in the same app\nSee https://fb.me/react-invalid-hook-call for tips about how to debug and fix this problem.");
            }
          }
          return dispatcher;
        }
        function useContext(Context, unstable_observedBits) {
          var dispatcher = resolveDispatcher();
          {
            if (unstable_observedBits !== void 0) {
              error("useContext() second argument is reserved for future use in React. Passing it is not supported. You passed: %s.%s", unstable_observedBits, typeof unstable_observedBits === "number" && Array.isArray(arguments[2]) ? "\n\nDid you call array.map(useContext)? Calling Hooks inside a loop is not supported. Learn more at https://fb.me/rules-of-hooks" : "");
            }
            if (Context._context !== void 0) {
              var realContext = Context._context;
              if (realContext.Consumer === Context) {
                error("Calling useContext(Context.Consumer) is not supported, may cause bugs, and will be removed in a future major release. Did you mean to call useContext(Context) instead?");
              } else if (realContext.Provider === Context) {
                error("Calling useContext(Context.Provider) is not supported. Did you mean to call useContext(Context) instead?");
              }
            }
          }
          return dispatcher.useContext(Context, unstable_observedBits);
        }
        function useState(initialState) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useState(initialState);
        }
        function useReducer(reducer, initialArg, init) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useReducer(reducer, initialArg, init);
        }
        function useRef(initialValue) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useRef(initialValue);
        }
        function useEffect(create, deps) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useEffect(create, deps);
        }
        function useLayoutEffect(create, deps) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useLayoutEffect(create, deps);
        }
        function useCallback(callback, deps) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useCallback(callback, deps);
        }
        function useMemo(create, deps) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useMemo(create, deps);
        }
        function useImperativeHandle(ref, create, deps) {
          var dispatcher = resolveDispatcher();
          return dispatcher.useImperativeHandle(ref, create, deps);
        }
        function useDebugValue(value, formatterFn) {
          {
            var dispatcher = resolveDispatcher();
            return dispatcher.useDebugValue(value, formatterFn);
          }
        }
        var propTypesMisspellWarningShown;
        {
          propTypesMisspellWarningShown = false;
        }
        function getDeclarationErrorAddendum() {
          if (ReactCurrentOwner.current) {
            var name = getComponentName(ReactCurrentOwner.current.type);
            if (name) {
              return "\n\nCheck the render method of `" + name + "`.";
            }
          }
          return "";
        }
        function getSourceInfoErrorAddendum(source) {
          if (source !== void 0) {
            var fileName = source.fileName.replace(/^.*[\\\/]/, "");
            var lineNumber = source.lineNumber;
            return "\n\nCheck your code at " + fileName + ":" + lineNumber + ".";
          }
          return "";
        }
        function getSourceInfoErrorAddendumForProps(elementProps) {
          if (elementProps !== null && elementProps !== void 0) {
            return getSourceInfoErrorAddendum(elementProps.__source);
          }
          return "";
        }
        var ownerHasKeyUseWarning = {};
        function getCurrentComponentErrorInfo(parentType) {
          var info = getDeclarationErrorAddendum();
          if (!info) {
            var parentName = typeof parentType === "string" ? parentType : parentType.displayName || parentType.name;
            if (parentName) {
              info = "\n\nCheck the top-level render call using <" + parentName + ">.";
            }
          }
          return info;
        }
        function validateExplicitKey(element, parentType) {
          if (!element._store || element._store.validated || element.key != null) {
            return;
          }
          element._store.validated = true;
          var currentComponentErrorInfo = getCurrentComponentErrorInfo(parentType);
          if (ownerHasKeyUseWarning[currentComponentErrorInfo]) {
            return;
          }
          ownerHasKeyUseWarning[currentComponentErrorInfo] = true;
          var childOwner = "";
          if (element && element._owner && element._owner !== ReactCurrentOwner.current) {
            childOwner = " It was passed a child from " + getComponentName(element._owner.type) + ".";
          }
          setCurrentlyValidatingElement(element);
          {
            error('Each child in a list should have a unique "key" prop.%s%s See https://fb.me/react-warning-keys for more information.', currentComponentErrorInfo, childOwner);
          }
          setCurrentlyValidatingElement(null);
        }
        function validateChildKeys(node, parentType) {
          if (typeof node !== "object") {
            return;
          }
          if (Array.isArray(node)) {
            for (var i = 0; i < node.length; i++) {
              var child = node[i];
              if (isValidElement(child)) {
                validateExplicitKey(child, parentType);
              }
            }
          } else if (isValidElement(node)) {
            if (node._store) {
              node._store.validated = true;
            }
          } else if (node) {
            var iteratorFn = getIteratorFn(node);
            if (typeof iteratorFn === "function") {
              if (iteratorFn !== node.entries) {
                var iterator = iteratorFn.call(node);
                var step;
                while (!(step = iterator.next()).done) {
                  if (isValidElement(step.value)) {
                    validateExplicitKey(step.value, parentType);
                  }
                }
              }
            }
          }
        }
        function validatePropTypes(element) {
          {
            var type = element.type;
            if (type === null || type === void 0 || typeof type === "string") {
              return;
            }
            var name = getComponentName(type);
            var propTypes;
            if (typeof type === "function") {
              propTypes = type.propTypes;
            } else if (typeof type === "object" && (type.$$typeof === REACT_FORWARD_REF_TYPE || // Note: Memo only checks outer props here.
            // Inner props are checked in the reconciler.
            type.$$typeof === REACT_MEMO_TYPE)) {
              propTypes = type.propTypes;
            } else {
              return;
            }
            if (propTypes) {
              setCurrentlyValidatingElement(element);
              checkPropTypes(propTypes, element.props, "prop", name, ReactDebugCurrentFrame.getStackAddendum);
              setCurrentlyValidatingElement(null);
            } else if (type.PropTypes !== void 0 && !propTypesMisspellWarningShown) {
              propTypesMisspellWarningShown = true;
              error("Component %s declared `PropTypes` instead of `propTypes`. Did you misspell the property assignment?", name || "Unknown");
            }
            if (typeof type.getDefaultProps === "function" && !type.getDefaultProps.isReactClassApproved) {
              error("getDefaultProps is only used on classic React.createClass definitions. Use a static property named `defaultProps` instead.");
            }
          }
        }
        function validateFragmentProps(fragment) {
          {
            setCurrentlyValidatingElement(fragment);
            var keys = Object.keys(fragment.props);
            for (var i = 0; i < keys.length; i++) {
              var key = keys[i];
              if (key !== "children" && key !== "key") {
                error("Invalid prop `%s` supplied to `React.Fragment`. React.Fragment can only have `key` and `children` props.", key);
                break;
              }
            }
            if (fragment.ref !== null) {
              error("Invalid attribute `ref` supplied to `React.Fragment`.");
            }
            setCurrentlyValidatingElement(null);
          }
        }
        function createElementWithValidation(type, props, children) {
          var validType = isValidElementType(type);
          if (!validType) {
            var info = "";
            if (type === void 0 || typeof type === "object" && type !== null && Object.keys(type).length === 0) {
              info += " You likely forgot to export your component from the file it's defined in, or you might have mixed up default and named imports.";
            }
            var sourceInfo = getSourceInfoErrorAddendumForProps(props);
            if (sourceInfo) {
              info += sourceInfo;
            } else {
              info += getDeclarationErrorAddendum();
            }
            var typeString;
            if (type === null) {
              typeString = "null";
            } else if (Array.isArray(type)) {
              typeString = "array";
            } else if (type !== void 0 && type.$$typeof === REACT_ELEMENT_TYPE) {
              typeString = "<" + (getComponentName(type.type) || "Unknown") + " />";
              info = " Did you accidentally export a JSX literal instead of a component?";
            } else {
              typeString = typeof type;
            }
            {
              error("React.createElement: type is invalid -- expected a string (for built-in components) or a class/function (for composite components) but got: %s.%s", typeString, info);
            }
          }
          var element = createElement.apply(this, arguments);
          if (element == null) {
            return element;
          }
          if (validType) {
            for (var i = 2; i < arguments.length; i++) {
              validateChildKeys(arguments[i], type);
            }
          }
          if (type === REACT_FRAGMENT_TYPE) {
            validateFragmentProps(element);
          } else {
            validatePropTypes(element);
          }
          return element;
        }
        var didWarnAboutDeprecatedCreateFactory = false;
        function createFactoryWithValidation(type) {
          var validatedFactory = createElementWithValidation.bind(null, type);
          validatedFactory.type = type;
          {
            if (!didWarnAboutDeprecatedCreateFactory) {
              didWarnAboutDeprecatedCreateFactory = true;
              warn("React.createFactory() is deprecated and will be removed in a future major release. Consider using JSX or use React.createElement() directly instead.");
            }
            Object.defineProperty(validatedFactory, "type", {
              enumerable: false,
              get: function() {
                warn("Factory.type is deprecated. Access the class directly before passing it to createFactory.");
                Object.defineProperty(this, "type", {
                  value: type
                });
                return type;
              }
            });
          }
          return validatedFactory;
        }
        function cloneElementWithValidation(element, props, children) {
          var newElement = cloneElement.apply(this, arguments);
          for (var i = 2; i < arguments.length; i++) {
            validateChildKeys(arguments[i], newElement.type);
          }
          validatePropTypes(newElement);
          return newElement;
        }
        {
          try {
            var frozenObject = Object.freeze({});
            var testMap = /* @__PURE__ */ new Map([[frozenObject, null]]);
            var testSet = /* @__PURE__ */ new Set([frozenObject]);
            testMap.set(0, 0);
            testSet.add(0);
          } catch (e) {
          }
        }
        var createElement$1 = createElementWithValidation;
        var cloneElement$1 = cloneElementWithValidation;
        var createFactory = createFactoryWithValidation;
        var Children = {
          map: mapChildren,
          forEach: forEachChildren,
          count: countChildren,
          toArray,
          only: onlyChild
        };
        exports.Children = Children;
        exports.Component = Component;
        exports.Fragment = REACT_FRAGMENT_TYPE;
        exports.Profiler = REACT_PROFILER_TYPE;
        exports.PureComponent = PureComponent;
        exports.StrictMode = REACT_STRICT_MODE_TYPE;
        exports.Suspense = REACT_SUSPENSE_TYPE;
        exports.__SECRET_INTERNALS_DO_NOT_USE_OR_YOU_WILL_BE_FIRED = ReactSharedInternals;
        exports.cloneElement = cloneElement$1;
        exports.createContext = createContext;
        exports.createElement = createElement$1;
        exports.createFactory = createFactory;
        exports.createRef = createRef;
        exports.forwardRef = forwardRef;
        exports.isValidElement = isValidElement;
        exports.lazy = lazy;
        exports.memo = memo;
        exports.useCallback = useCallback;
        exports.useContext = useContext;
        exports.useDebugValue = useDebugValue;
        exports.useEffect = useEffect;
        exports.useImperativeHandle = useImperativeHandle;
        exports.useLayoutEffect = useLayoutEffect;
        exports.useMemo = useMemo;
        exports.useReducer = useReducer;
        exports.useRef = useRef;
        exports.useState = useState;
        exports.version = ReactVersion;
      })();
    }
  }
});

// node_modules/@react-latest-ui/react-sticky-notes/node_modules/react/index.js
var require_react = __commonJS({
  "node_modules/@react-latest-ui/react-sticky-notes/node_modules/react/index.js"(exports, module) {
    "use strict";
    if (false) {
      module.exports = null;
    } else {
      module.exports = require_react_development();
    }
  }
});

// node_modules/@react-latest-ui/react-sticky-notes/build/index.js
var require_build = __commonJS({
  "node_modules/@react-latest-ui/react-sticky-notes/build/index.js"(exports, module) {
    module.exports = /******/
    function(modules) {
      var installedModules = {};
      function __webpack_require__(moduleId) {
        if (installedModules[moduleId]) {
          return installedModules[moduleId].exports;
        }
        var module2 = installedModules[moduleId] = {
          /******/
          i: moduleId,
          /******/
          l: false,
          /******/
          exports: {}
          /******/
        };
        modules[moduleId].call(module2.exports, module2, module2.exports, __webpack_require__);
        module2.l = true;
        return module2.exports;
      }
      __webpack_require__.m = modules;
      __webpack_require__.c = installedModules;
      __webpack_require__.d = function(exports2, name, getter) {
        if (!__webpack_require__.o(exports2, name)) {
          Object.defineProperty(exports2, name, { enumerable: true, get: getter });
        }
      };
      __webpack_require__.r = function(exports2) {
        if (typeof Symbol !== "undefined" && Symbol.toStringTag) {
          Object.defineProperty(exports2, Symbol.toStringTag, { value: "Module" });
        }
        Object.defineProperty(exports2, "__esModule", { value: true });
      };
      __webpack_require__.t = function(value, mode) {
        if (mode & 1) value = __webpack_require__(value);
        if (mode & 8) return value;
        if (mode & 4 && typeof value === "object" && value && value.__esModule) return value;
        var ns = /* @__PURE__ */ Object.create(null);
        __webpack_require__.r(ns);
        Object.defineProperty(ns, "default", { enumerable: true, value });
        if (mode & 2 && typeof value != "string") for (var key in value) __webpack_require__.d(ns, key, (function(key2) {
          return value[key2];
        }).bind(null, key));
        return ns;
      };
      __webpack_require__.n = function(module2) {
        var getter = module2 && module2.__esModule ? (
          /******/
          function getDefault() {
            return module2["default"];
          }
        ) : (
          /******/
          function getModuleExports() {
            return module2;
          }
        );
        __webpack_require__.d(getter, "a", getter);
        return getter;
      };
      __webpack_require__.o = function(object, property) {
        return Object.prototype.hasOwnProperty.call(object, property);
      };
      __webpack_require__.p = "";
      return __webpack_require__(__webpack_require__.s = "./src/index.js");
    }({
      /***/
      "./node_modules/css-loader/index.js!./node_modules/sass-loader/lib/loader.js!./src/components/react-sticky-notes/index.scss": (
        /*!*************************************************************************************************************************!*\
          !*** ./node_modules/css-loader!./node_modules/sass-loader/lib/loader.js!./src/components/react-sticky-notes/index.scss ***!
          \*************************************************************************************************************************/
        /*! no static exports found */
        /***/
        function(module2, exports2, __webpack_require__) {
          exports2 = module2.exports = __webpack_require__(
            /*! ../../../node_modules/css-loader/lib/css-base.js */
            "./node_modules/css-loader/lib/css-base.js"
          )(false);
          exports2.push([module2.i, "@keyframes shadowanim {\n  0% {\n    box-shadow: 0px 0px 0px 25px inset rgba(0, 0, 0, 0.15), 0px 0px 0px 15px inset rgba(0, 0, 0, 0.15), 0px 0px 0px 5px inset rgba(0, 0, 0, 0.15);\n    opacity: 0.2; }\n  20% {\n    opacity: 0.9; }\n  50% {\n    opacity: 1; }\n  70% {\n    opacity: 0.9; }\n  100% {\n    box-shadow: 0px 0px 0px 0px inset rgba(0, 0, 0, 0.15);\n    opacity: 0.2; } }\n\n.rs-notes {\n  color: #ffffff;\n  text-align: left; }\n  .rs-notes *, .rs-notes *::before, .rs-notes *::after {\n    box-sizing: border-box; }\n  .rs-notes--note.draggable {\n    z-index: 9; }\n  .rs-notes--navbar {\n    white-space: nowrap;\n    display: flex;\n    justify-content: space-between;\n    background-color: #999999; }\n    @media screen and (max-width: 800px) {\n      .rs-notes--navbar {\n        flex-direction: column-reverse; } }\n    .rs-notes--navbar__nav {\n      display: flex;\n      flex-wrap: wrap; }\n    .rs-notes--navbar__options {\n      display: flex; }\n    .rs-notes--navbar__item {\n      display: flex;\n      flex-grow: 1;\n      position: relative;\n      vertical-align: middle;\n      overflow: hidden; }\n      .rs-notes--navbar__item:before {\n        content: '';\n        width: 100%;\n        height: 100%;\n        position: absolute;\n        left: 0;\n        top: 0;\n        pointer-events: none;\n        background-color: rgba(0, 0, 0, 0.15); }\n      .rs-notes--navbar__item--button {\n        cursor: pointer;\n        opacity: .8;\n        background: none;\n        border: none;\n        color: #fff;\n        padding: 3px 5px; }\n        .rs-notes--navbar__item--button__title {\n          text-align: left;\n          flex-grow: 1;\n          min-width: 80px; }\n        .rs-notes--navbar__item--button:hover, .rs-notes--navbar__item--button:focus {\n          opacity: 1;\n          background-color: rgba(0, 0, 0, 0.15); }\n  .rs-notes--header {\n    position: relative;\n    display: flex;\n    transition: all .3s linear;\n    border-bottom: 1px solid rgba(0, 0, 0, 0.25); }\n    .rs-notes--header svg {\n      line-height: 1;\n      vertical-align: middle; }\n    .rs-notes--header:before {\n      content: '';\n      width: 100%;\n      height: 100%;\n      position: absolute;\n      left: 0;\n      top: 0;\n      pointer-events: none;\n      background-color: rgba(0, 0, 0, 0.15); }\n    .rs-notes--header--button {\n      cursor: pointer;\n      line-height: 30px;\n      background: none;\n      border: none;\n      transition: all .2s linear;\n      padding: 5px;\n      color: rgba(255, 255, 255, 0.75);\n      width: 32px; }\n      .rs-notes--header--button__title {\n        flex-grow: 1;\n        line-height: 30px;\n        text-align: left;\n        user-select: none;\n        cursor: move; }\n      .rs-notes--header--button:hover, .rs-notes--header--button:focus {\n        background-color: rgba(0, 0, 0, 0.25);\n        outline: none; }\n      .rs-notes--header--button:disabled {\n        cursor: not-allowed; }\n  .rs-notes--text {\n    padding: 10px;\n    font-size: 12px;\n    width: 100%; }\n    .rs-notes--text:empty::before {\n      color: rgba(255, 255, 255, 0.75);\n      content: 'Add your notes...'; }\n  .rs-notes--colors {\n    flex-grow: 1; }\n    .rs-notes--colors__color {\n      text-indent: -99999px;\n      cursor: pointer;\n      margin: 1px;\n      width: 3.6em;\n      height: 3.6em;\n      border-radius: 50%;\n      border: none;\n      outline: none;\n      transition: all 0.4s linear;\n      box-shadow: 0px 0px 0px 5px inset rgba(0, 0, 0, 0.15); }\n      .rs-notes--colors__color:hover, .rs-notes--colors__color:focus {\n        box-shadow: 0px 0px 0px 10px inset rgba(0, 0, 0, 0.15); }\n      .rs-notes--colors__color--selected {\n        animation-name: shadowanim;\n        animation-duration: 2s;\n        animation-iteration-count: 100;\n        opacity: .75; }\n  .rs-notes--note__bubble {\n    cursor: move;\n    border: none;\n    outline: none;\n    position: relative; }\n    .rs-notes--note__bubble::before {\n      display: block;\n      opacity: 0;\n      content: attr(title);\n      transform: translate(-50%, -50%);\n      transform-origin: 0 0;\n      transition: all linear .4s;\n      overflow: hidden;\n      background-color: var(--background-color);\n      white-space: nowrap;\n      position: absolute;\n      left: 50%;\n      top: 50%;\n      padding: 5px 8px;\n      border-radius: 5px;\n      color: rgba(255, 255, 255, 0.75);\n      font-size: .9em; }\n    .rs-notes--note__bubble:hover::before {\n      opacity: 1; }\n    .rs-notes--note__bubble:focus, .rs-notes--note__bubble:active {\n      opacity: .5;\n      z-index: 9999; }\n  .rs-notes--file-upload {\n    position: relative;\n    min-height: 100%;\n    display: flex;\n    flex-direction: column; }\n  .rs-notes p {\n    margin: 5px 0; }\n  .rs-notes--file-preview {\n    text-align: left;\n    overflow: auto;\n    width: 100%;\n    border: none;\n    resize: none;\n    flex-grow: 1;\n    min-height: 150px; }\n  .rs-notes--file-drop {\n    padding: 30px;\n    position: relative;\n    background-color: #eeeeee;\n    color: #999999;\n    outline: 2px dashed #999999;\n    outline-offset: -10px;\n    transition: outline-offset .15s ease-in-out, background-color .15s linear;\n    text-align: center; }\n    .rs-notes--file-drop:hover {\n      outline-offset: -20px;\n      outline-color: #cccccc;\n      background-color: #ffffff; }\n    .rs-notes--file-drop__cover {\n      flex-grow: 1; }\n  .rs-notes--upload-link {\n    font-size: 1.25rem; }\n  .rs-notes--file-input {\n    width: 100%;\n    height: 100%;\n    opacity: 0;\n    overflow: hidden;\n    position: absolute;\n    left: 0;\n    top: 0;\n    z-index: 0; }\n  .rs-notes--file-label {\n    position: relative;\n    cursor: pointer;\n    color: #666666; }\n    .rs-notes--file-label:hover {\n      color: #999999; }\n  .rs-notes--upload-actions {\n    padding: 24px; }\n  .rs-notes--form-cancel, .rs-notes--form-save {\n    padding: 8px 15px;\n    border: none;\n    background: #999999;\n    color: #ffffff;\n    cursor: pointer; }\n    .rs-notes--form-cancel:hover, .rs-notes--form-save:hover {\n      background: rgba(0, 0, 0, 0.15); }\n  .rs-notes--upload-error {\n    background-color: #cc0000;\n    font-size: .85em;\n    color: #fff;\n    padding: 3px 5px; }\n  .rs-notes--upload-actions {\n    text-align: center; }\n  .rs-notes--notes-area {\n    position: relative;\n    width: calc(100% - 15px);\n    height: 100%;\n    margin-left: 15px;\n    z-index: 1; }\n  .rs-notes--notes-colors {\n    position: absolute;\n    width: 100%;\n    height: 100%;\n    z-index: 0; }\n    .rs-notes--notes-colors__color {\n      position: relative;\n      opacity: 0.8;\n      width: 100%;\n      height: var(--height);\n      border-bottom: 1px solid var(--background-color); }\n      .rs-notes--notes-colors__color::before {\n        background-color: var(--background-color);\n        width: 15px;\n        height: 100%;\n        display: block;\n        position: absolute;\n        top: 1px;\n        left: 0;\n        content: ''; }\n", ""]);
        }
      ),
      /***/
      "./node_modules/css-loader/lib/css-base.js": (
        /*!*************************************************!*\
          !*** ./node_modules/css-loader/lib/css-base.js ***!
          \*************************************************/
        /*! no static exports found */
        /***/
        function(module2, exports2) {
          module2.exports = function(useSourceMap) {
            var list = [];
            list.toString = function toString() {
              return this.map(function(item) {
                var content = cssWithMappingToString(item, useSourceMap);
                if (item[2]) {
                  return "@media " + item[2] + "{" + content + "}";
                } else {
                  return content;
                }
              }).join("");
            };
            list.i = function(modules, mediaQuery) {
              if (typeof modules === "string")
                modules = [[null, modules, ""]];
              var alreadyImportedModules = {};
              for (var i = 0; i < this.length; i++) {
                var id = this[i][0];
                if (typeof id === "number")
                  alreadyImportedModules[id] = true;
              }
              for (i = 0; i < modules.length; i++) {
                var item = modules[i];
                if (typeof item[0] !== "number" || !alreadyImportedModules[item[0]]) {
                  if (mediaQuery && !item[2]) {
                    item[2] = mediaQuery;
                  } else if (mediaQuery) {
                    item[2] = "(" + item[2] + ") and (" + mediaQuery + ")";
                  }
                  list.push(item);
                }
              }
            };
            return list;
          };
          function cssWithMappingToString(item, useSourceMap) {
            var content = item[1] || "";
            var cssMapping = item[3];
            if (!cssMapping) {
              return content;
            }
            if (useSourceMap && typeof btoa === "function") {
              var sourceMapping = toComment(cssMapping);
              var sourceURLs = cssMapping.sources.map(function(source) {
                return "/*# sourceURL=" + cssMapping.sourceRoot + source + " */";
              });
              return [content].concat(sourceURLs).concat([sourceMapping]).join("\n");
            }
            return [content].join("\n");
          }
          function toComment(sourceMap) {
            var base64 = btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap))));
            var data = "sourceMappingURL=data:application/json;charset=utf-8;base64," + base64;
            return "/*# " + data + " */";
          }
        }
      ),
      /***/
      "./node_modules/style-loader/lib/addStyles.js": (
        /*!****************************************************!*\
          !*** ./node_modules/style-loader/lib/addStyles.js ***!
          \****************************************************/
        /*! no static exports found */
        /***/
        function(module2, exports2, __webpack_require__) {
          var stylesInDom = {};
          var memoize = function(fn) {
            var memo;
            return function() {
              if (typeof memo === "undefined") memo = fn.apply(this, arguments);
              return memo;
            };
          };
          var isOldIE = memoize(function() {
            return window && document && document.all && !window.atob;
          });
          var getTarget = function(target, parent) {
            if (parent) {
              return parent.querySelector(target);
            }
            return document.querySelector(target);
          };
          var getElement = /* @__PURE__ */ function(fn) {
            var memo = {};
            return function(target, parent) {
              if (typeof target === "function") {
                return target();
              }
              if (typeof memo[target] === "undefined") {
                var styleTarget = getTarget.call(this, target, parent);
                if (window.HTMLIFrameElement && styleTarget instanceof window.HTMLIFrameElement) {
                  try {
                    styleTarget = styleTarget.contentDocument.head;
                  } catch (e) {
                    styleTarget = null;
                  }
                }
                memo[target] = styleTarget;
              }
              return memo[target];
            };
          }();
          var singleton = null;
          var singletonCounter = 0;
          var stylesInsertedAtTop = [];
          var fixUrls = __webpack_require__(
            /*! ./urls */
            "./node_modules/style-loader/lib/urls.js"
          );
          module2.exports = function(list, options) {
            if (typeof DEBUG !== "undefined" && DEBUG) {
              if (typeof document !== "object") throw new Error("The style-loader cannot be used in a non-browser environment");
            }
            options = options || {};
            options.attrs = typeof options.attrs === "object" ? options.attrs : {};
            if (!options.singleton && typeof options.singleton !== "boolean") options.singleton = isOldIE();
            if (!options.insertInto) options.insertInto = "head";
            if (!options.insertAt) options.insertAt = "bottom";
            var styles = listToStyles(list, options);
            addStylesToDom(styles, options);
            return function update(newList) {
              var mayRemove = [];
              for (var i = 0; i < styles.length; i++) {
                var item = styles[i];
                var domStyle = stylesInDom[item.id];
                domStyle.refs--;
                mayRemove.push(domStyle);
              }
              if (newList) {
                var newStyles = listToStyles(newList, options);
                addStylesToDom(newStyles, options);
              }
              for (var i = 0; i < mayRemove.length; i++) {
                var domStyle = mayRemove[i];
                if (domStyle.refs === 0) {
                  for (var j = 0; j < domStyle.parts.length; j++) domStyle.parts[j]();
                  delete stylesInDom[domStyle.id];
                }
              }
            };
          };
          function addStylesToDom(styles, options) {
            for (var i = 0; i < styles.length; i++) {
              var item = styles[i];
              var domStyle = stylesInDom[item.id];
              if (domStyle) {
                domStyle.refs++;
                for (var j = 0; j < domStyle.parts.length; j++) {
                  domStyle.parts[j](item.parts[j]);
                }
                for (; j < item.parts.length; j++) {
                  domStyle.parts.push(addStyle(item.parts[j], options));
                }
              } else {
                var parts = [];
                for (var j = 0; j < item.parts.length; j++) {
                  parts.push(addStyle(item.parts[j], options));
                }
                stylesInDom[item.id] = { id: item.id, refs: 1, parts };
              }
            }
          }
          function listToStyles(list, options) {
            var styles = [];
            var newStyles = {};
            for (var i = 0; i < list.length; i++) {
              var item = list[i];
              var id = options.base ? item[0] + options.base : item[0];
              var css = item[1];
              var media = item[2];
              var sourceMap = item[3];
              var part = { css, media, sourceMap };
              if (!newStyles[id]) styles.push(newStyles[id] = { id, parts: [part] });
              else newStyles[id].parts.push(part);
            }
            return styles;
          }
          function insertStyleElement(options, style) {
            var target = getElement(options.insertInto);
            if (!target) {
              throw new Error("Couldn't find a style target. This probably means that the value for the 'insertInto' parameter is invalid.");
            }
            var lastStyleElementInsertedAtTop = stylesInsertedAtTop[stylesInsertedAtTop.length - 1];
            if (options.insertAt === "top") {
              if (!lastStyleElementInsertedAtTop) {
                target.insertBefore(style, target.firstChild);
              } else if (lastStyleElementInsertedAtTop.nextSibling) {
                target.insertBefore(style, lastStyleElementInsertedAtTop.nextSibling);
              } else {
                target.appendChild(style);
              }
              stylesInsertedAtTop.push(style);
            } else if (options.insertAt === "bottom") {
              target.appendChild(style);
            } else if (typeof options.insertAt === "object" && options.insertAt.before) {
              var nextSibling = getElement(options.insertAt.before, target);
              target.insertBefore(style, nextSibling);
            } else {
              throw new Error("[Style Loader]\n\n Invalid value for parameter 'insertAt' ('options.insertAt') found.\n Must be 'top', 'bottom', or Object.\n (https://github.com/webpack-contrib/style-loader#insertat)\n");
            }
          }
          function removeStyleElement(style) {
            if (style.parentNode === null) return false;
            style.parentNode.removeChild(style);
            var idx = stylesInsertedAtTop.indexOf(style);
            if (idx >= 0) {
              stylesInsertedAtTop.splice(idx, 1);
            }
          }
          function createStyleElement(options) {
            var style = document.createElement("style");
            if (options.attrs.type === void 0) {
              options.attrs.type = "text/css";
            }
            if (options.attrs.nonce === void 0) {
              var nonce = getNonce();
              if (nonce) {
                options.attrs.nonce = nonce;
              }
            }
            addAttrs(style, options.attrs);
            insertStyleElement(options, style);
            return style;
          }
          function createLinkElement(options) {
            var link = document.createElement("link");
            if (options.attrs.type === void 0) {
              options.attrs.type = "text/css";
            }
            options.attrs.rel = "stylesheet";
            addAttrs(link, options.attrs);
            insertStyleElement(options, link);
            return link;
          }
          function addAttrs(el, attrs) {
            Object.keys(attrs).forEach(function(key) {
              el.setAttribute(key, attrs[key]);
            });
          }
          function getNonce() {
            if (false) {
            }
            return __webpack_require__.nc;
          }
          function addStyle(obj, options) {
            var style, update, remove, result;
            if (options.transform && obj.css) {
              result = options.transform(obj.css);
              if (result) {
                obj.css = result;
              } else {
                return function() {
                };
              }
            }
            if (options.singleton) {
              var styleIndex = singletonCounter++;
              style = singleton || (singleton = createStyleElement(options));
              update = applyToSingletonTag.bind(null, style, styleIndex, false);
              remove = applyToSingletonTag.bind(null, style, styleIndex, true);
            } else if (obj.sourceMap && typeof URL === "function" && typeof URL.createObjectURL === "function" && typeof URL.revokeObjectURL === "function" && typeof Blob === "function" && typeof btoa === "function") {
              style = createLinkElement(options);
              update = updateLink.bind(null, style, options);
              remove = function() {
                removeStyleElement(style);
                if (style.href) URL.revokeObjectURL(style.href);
              };
            } else {
              style = createStyleElement(options);
              update = applyToTag.bind(null, style);
              remove = function() {
                removeStyleElement(style);
              };
            }
            update(obj);
            return function updateStyle(newObj) {
              if (newObj) {
                if (newObj.css === obj.css && newObj.media === obj.media && newObj.sourceMap === obj.sourceMap) {
                  return;
                }
                update(obj = newObj);
              } else {
                remove();
              }
            };
          }
          var replaceText = /* @__PURE__ */ function() {
            var textStore = [];
            return function(index, replacement) {
              textStore[index] = replacement;
              return textStore.filter(Boolean).join("\n");
            };
          }();
          function applyToSingletonTag(style, index, remove, obj) {
            var css = remove ? "" : obj.css;
            if (style.styleSheet) {
              style.styleSheet.cssText = replaceText(index, css);
            } else {
              var cssNode = document.createTextNode(css);
              var childNodes = style.childNodes;
              if (childNodes[index]) style.removeChild(childNodes[index]);
              if (childNodes.length) {
                style.insertBefore(cssNode, childNodes[index]);
              } else {
                style.appendChild(cssNode);
              }
            }
          }
          function applyToTag(style, obj) {
            var css = obj.css;
            var media = obj.media;
            if (media) {
              style.setAttribute("media", media);
            }
            if (style.styleSheet) {
              style.styleSheet.cssText = css;
            } else {
              while (style.firstChild) {
                style.removeChild(style.firstChild);
              }
              style.appendChild(document.createTextNode(css));
            }
          }
          function updateLink(link, options, obj) {
            var css = obj.css;
            var sourceMap = obj.sourceMap;
            var autoFixUrls = options.convertToAbsoluteUrls === void 0 && sourceMap;
            if (options.convertToAbsoluteUrls || autoFixUrls) {
              css = fixUrls(css);
            }
            if (sourceMap) {
              css += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(sourceMap)))) + " */";
            }
            var blob = new Blob([css], { type: "text/css" });
            var oldSrc = link.href;
            link.href = URL.createObjectURL(blob);
            if (oldSrc) URL.revokeObjectURL(oldSrc);
          }
        }
      ),
      /***/
      "./node_modules/style-loader/lib/urls.js": (
        /*!***********************************************!*\
          !*** ./node_modules/style-loader/lib/urls.js ***!
          \***********************************************/
        /*! no static exports found */
        /***/
        function(module2, exports2) {
          module2.exports = function(css) {
            var location = typeof window !== "undefined" && window.location;
            if (!location) {
              throw new Error("fixUrls requires window.location");
            }
            if (!css || typeof css !== "string") {
              return css;
            }
            var baseUrl = location.protocol + "//" + location.host;
            var currentDir = baseUrl + location.pathname.replace(/\/[^\/]*$/, "/");
            var fixedCss = css.replace(/url\s*\(((?:[^)(]|\((?:[^)(]+|\([^)(]*\))*\))*)\)/gi, function(fullMatch, origUrl) {
              var unquotedOrigUrl = origUrl.trim().replace(/^"(.*)"$/, function(o, $1) {
                return $1;
              }).replace(/^'(.*)'$/, function(o, $1) {
                return $1;
              });
              if (/^(#|data:|http:\/\/|https:\/\/|file:\/\/\/|\s*$)/i.test(unquotedOrigUrl)) {
                return fullMatch;
              }
              var newUrl;
              if (unquotedOrigUrl.indexOf("//") === 0) {
                newUrl = unquotedOrigUrl;
              } else if (unquotedOrigUrl.indexOf("/") === 0) {
                newUrl = baseUrl + unquotedOrigUrl;
              } else {
                newUrl = currentDir + unquotedOrigUrl.replace(/^\.\//, "");
              }
              return "url(" + JSON.stringify(newUrl) + ")";
            });
            return fixedCss;
          };
        }
      ),
      /***/
      "./src/components/react-sticky-notes/buttons/index.js": (
        /*!************************************************************!*\
          !*** ./src/components/react-sticky-notes/buttons/index.js ***!
          \************************************************************/
        /*! exports provided: ButtonAdd, ButtonTitle, ButtonMenu, ButtonHideShow, ButtonTrash, ButtonPageView, ButtonUpload */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "ButtonAdd", function() {
            return ButtonAdd;
          });
          __webpack_require__.d(__webpack_exports__, "ButtonTitle", function() {
            return ButtonTitle;
          });
          __webpack_require__.d(__webpack_exports__, "ButtonMenu", function() {
            return ButtonMenu;
          });
          __webpack_require__.d(__webpack_exports__, "ButtonHideShow", function() {
            return ButtonHideShow;
          });
          __webpack_require__.d(__webpack_exports__, "ButtonTrash", function() {
            return ButtonTrash;
          });
          __webpack_require__.d(__webpack_exports__, "ButtonPageView", function() {
            return ButtonPageView;
          });
          __webpack_require__.d(__webpack_exports__, "ButtonUpload", function() {
            return ButtonUpload;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          function ButtonAdd(_ref) {
            var prefix = _ref.prefix, data = _ref.data, icons = _ref.icons, callbacks = _ref.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__add"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__add"),
              onClick: function onClick(e) {
                return callbacks.addItem(e, {
                  id: data ? data.id : null,
                  position: data ? data.position : null,
                  selected: true
                });
              }
            }, icons.add);
          }
          function ButtonTitle(_ref2) {
            var prefix = _ref2.prefix, data = _ref2.data, targetRef = _ref2.targetRef, callbacks = _ref2.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__title"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__title"),
              ref: targetRef,
              onClick: function onClick(e) {
                return callbacks.updateItem(e, {
                  id: data ? data.id : null,
                  menu: false,
                  selected: true,
                  hidden: false
                });
              }
            }, data.title ? data.title : Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getNoteTitle"])(data));
          }
          function ButtonMenu(_ref3) {
            var prefix = _ref3.prefix, data = _ref3.data, icons = _ref3.icons, callbacks = _ref3.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__menu"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__menu"),
              onClick: function onClick(e) {
                return callbacks.updateItem(e, {
                  id: data ? data.id : null,
                  menu: !data.menu,
                  selected: true
                });
              }
            }, icons.menu);
          }
          function ButtonHideShow(_ref4) {
            var prefix = _ref4.prefix, data = _ref4.data, icons = _ref4.icons, callbacks = _ref4.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__hideshow"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__hideshow"),
              onClick: function onClick(e) {
                return callbacks.updateItem(e, {
                  id: data ? data.id : null,
                  hidden: !data.hidden
                });
              }
            }, data.hidden ? icons.hide : icons.show);
          }
          function ButtonTrash(_ref5) {
            var prefix = _ref5.prefix, data = _ref5.data, icons = _ref5.icons, callbacks = _ref5.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__trash"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__trash"),
              onClick: function onClick(e) {
                return callbacks.deleteItem(e, {
                  id: data ? data.id : null
                });
              }
            }, icons.trash);
          }
          function ButtonPageView(_ref6) {
            var prefix = _ref6.prefix, icons = _ref6.icons, callbacks = _ref6.callbacks, viewSize = _ref6.viewSize;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__pageview"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__pageview"),
              onClick: function onClick(e) {
                return callbacks.changeView(e);
              }
            }, icons[viewSize] ? icons[viewSize] : "icons.".concat(viewSize));
          }
          function ButtonUpload(_ref7) {
            var prefix = _ref7.prefix, icons = _ref7.icons, callbacks = _ref7.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
              key: "".concat(prefix, "--button__upload"),
              className: "".concat(prefix, "--button ").concat(prefix, "--button__upload"),
              onClick: function onClick(e) {
                return callbacks.changeModal(e, "upload");
              }
            }, icons.upload);
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/icons/index.js": (
        /*!**********************************************************!*\
          !*** ./src/components/react-sticky-notes/icons/index.js ***!
          \**********************************************************/
        /*! exports provided: add, trash, menu, hide, show, normalview, bubbleview, pageview, upload, fullscreen */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "add", function() {
            return add;
          });
          __webpack_require__.d(__webpack_exports__, "trash", function() {
            return trash;
          });
          __webpack_require__.d(__webpack_exports__, "menu", function() {
            return menu;
          });
          __webpack_require__.d(__webpack_exports__, "hide", function() {
            return hide;
          });
          __webpack_require__.d(__webpack_exports__, "show", function() {
            return show;
          });
          __webpack_require__.d(__webpack_exports__, "normalview", function() {
            return normalview;
          });
          __webpack_require__.d(__webpack_exports__, "bubbleview", function() {
            return bubbleview;
          });
          __webpack_require__.d(__webpack_exports__, "pageview", function() {
            return pageview;
          });
          __webpack_require__.d(__webpack_exports__, "upload", function() {
            return upload;
          });
          __webpack_require__.d(__webpack_exports__, "fullscreen", function() {
            return fullscreen;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var iconsClassName = "material-icons";
          var add = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "add");
          var trash = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "delete_outlined");
          var menu = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "more_horiz");
          var hide = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "visibility_off");
          var show = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "minimize");
          var normalview = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "widgets");
          var bubbleview = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "grain");
          var pageview = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "fullscreen");
          var upload = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "cloud_upload");
          var fullscreen = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("i", {
            className: iconsClassName,
            style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("icon")
          }, "fullscreen_exit");
        }
      ),
      /***/
      "./src/components/react-sticky-notes/index.js": (
        /*!****************************************************!*\
          !*** ./src/components/react-sticky-notes/index.js ***!
          \****************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_0___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
          var _reducers_reducer__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./reducers/reducer */
            "./src/components/react-sticky-notes/reducers/reducer.js"
          );
          var _icons__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./icons */
            "./src/components/react-sticky-notes/icons/index.js"
          );
          var _utils__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
            /*! ./utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _views__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
            /*! ./views */
            "./src/components/react-sticky-notes/views/index.js"
          );
          var _modals__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
            /*! ./modals */
            "./src/components/react-sticky-notes/modals/index.js"
          );
          function _typeof(obj) {
            if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
              _typeof = function _typeof2(obj2) {
                return typeof obj2;
              };
            } else {
              _typeof = function _typeof2(obj2) {
                return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
              };
            }
            return _typeof(obj);
          }
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _toConsumableArray(arr) {
            return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _nonIterableSpread();
          }
          function _nonIterableSpread() {
            throw new TypeError("Invalid attempt to spread non-iterable instance");
          }
          function _iterableToArray(iter) {
            if (Symbol.iterator in Object(iter) || Object.prototype.toString.call(iter) === "[object Arguments]") return Array.from(iter);
          }
          function _arrayWithoutHoles(arr) {
            if (Array.isArray(arr)) {
              for (var i = 0, arr2 = new Array(arr.length); i < arr.length; i++) {
                arr2[i] = arr[i];
              }
              return arr2;
            }
          }
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _possibleConstructorReturn(self, call) {
            if (call && (_typeof(call) === "object" || typeof call === "function")) {
              return call;
            }
            return _assertThisInitialized(self);
          }
          function _getPrototypeOf(o) {
            _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf2(o2) {
              return o2.__proto__ || Object.getPrototypeOf(o2);
            };
            return _getPrototypeOf(o);
          }
          function _assertThisInitialized(self) {
            if (self === void 0) {
              throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
            }
            return self;
          }
          function _inherits(subClass, superClass) {
            if (typeof superClass !== "function" && superClass !== null) {
              throw new TypeError("Super expression must either be null or a function");
            }
            subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } });
            if (superClass) _setPrototypeOf(subClass, superClass);
          }
          function _setPrototypeOf(o, p) {
            _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf2(o2, p2) {
              o2.__proto__ = p2;
              return o2;
            };
            return _setPrototypeOf(o, p);
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          var ReactStickyNotes = function(_Component) {
            _inherits(ReactStickyNotes2, _Component);
            function ReactStickyNotes2(props) {
              var _this;
              _classCallCheck(this, ReactStickyNotes2);
              _this = _possibleConstructorReturn(this, _getPrototypeOf(ReactStickyNotes2).call(this, props));
              _defineProperty(_assertThisInitialized(_this), "dispatch", function(options) {
                var type = options.type, payload = options.payload;
                if (_this.props.onBeforeChange) {
                  payload = _this.props.onBeforeChange(type, payload, _toConsumableArray(_this.state.items));
                }
                _this.setState(Object(_reducers_reducer__WEBPACK_IMPORTED_MODULE_1__["default"])(_this.state, {
                  type,
                  payload
                }), function() {
                  if (_this.props.sessionKey) {
                    localStorage.setItem(_this.props.sessionKey, JSON.stringify(_this.state.items));
                  }
                  if (_this.props.onChange) {
                    _this.props.onChange(type, payload, _toConsumableArray(_this.state.items));
                  }
                });
              });
              _defineProperty(_assertThisInitialized(_this), "addItem", function(e, data) {
                var items = _this.state.items;
                var index = data ? items.findIndex(function(item) {
                  return item.id === data.id;
                }) + 1 : items.length;
                _this.dispatch({
                  type: "add",
                  payload: {
                    index,
                    data: {
                      id: Object(_utils__WEBPACK_IMPORTED_MODULE_3__["getUUID"])(),
                      color: _this.getColor(),
                      text: "",
                      selected: true,
                      position: {
                        x: 0,
                        y: 0
                      }
                    }
                  }
                });
              });
              _defineProperty(_assertThisInitialized(_this), "updateItem", function(e, data) {
                _this.dispatch({
                  type: "update",
                  payload: {
                    data
                  }
                });
              });
              _defineProperty(_assertThisInitialized(_this), "deleteItem", function(e, data) {
                _this.dispatch({
                  type: "delete",
                  payload: {
                    data
                  }
                });
              });
              _defineProperty(_assertThisInitialized(_this), "changeView", function(e) {
                _this.dispatch({
                  type: "changeview"
                });
              });
              _defineProperty(_assertThisInitialized(_this), "changeModal", function(e, modal) {
                _this.dispatch({
                  type: "changemodal",
                  payload: {
                    modal
                  }
                });
              });
              _defineProperty(_assertThisInitialized(_this), "saveJSON", function(e, json) {
                _this.dispatch({
                  type: "import",
                  payload: {
                    items: Object(_utils__WEBPACK_IMPORTED_MODULE_3__["getNotes"])(_this.props.colorCodes, json)
                  }
                });
              });
              _this.state = {
                modal: null,
                viewSize: "normalview",
                items: Object(_utils__WEBPACK_IMPORTED_MODULE_3__["getNotes"])(props.colorCodes, props.notes)
              };
              return _this;
            }
            _createClass(ReactStickyNotes2, [{
              key: "componentDidMount",
              value: function componentDidMount() {
                if (this.props.useCSS) {
                  __webpack_require__(
                    /*! ./index.scss */
                    "./src/components/react-sticky-notes/index.scss"
                  );
                }
                if (this.props.useMaterialIcons) {
                  var stylesheet = document.createElement("link");
                  stylesheet.href = "https://fonts.googleapis.com/icon?family=Material+Icons";
                  stylesheet.rel = "stylesheet";
                  stylesheet.id = "material-icons-css";
                  if (!document.getElementById("material-icons-css")) {
                    document.head.appendChild(stylesheet);
                  }
                }
              }
            }, {
              key: "getColor",
              value: function getColor() {
                return this.props.colorCodes[Math.floor(Math.random() * this.props.colorCodes.length)];
              }
            }, {
              key: "render",
              value: function render() {
                var _this$state = this.state, items = _this$state.items, viewSize = _this$state.viewSize, modal = _this$state.modal;
                var View = null;
                if (modal) {
                  switch (modal) {
                    case "upload":
                      View = _modals__WEBPACK_IMPORTED_MODULE_5__["UploadModal"];
                      break;
                  }
                } else {
                  switch (viewSize) {
                    case "pageview":
                      View = _views__WEBPACK_IMPORTED_MODULE_4__["PageView"];
                      break;
                    case "bubbleview":
                      View = _views__WEBPACK_IMPORTED_MODULE_4__["BubbleView"];
                      break;
                    case "fullscreen":
                      View = _views__WEBPACK_IMPORTED_MODULE_4__["FullscreenView"];
                      break;
                    default:
                      View = _views__WEBPACK_IMPORTED_MODULE_4__["NormalView"];
                      break;
                  }
                }
                return Object(_utils__WEBPACK_IMPORTED_MODULE_3__["h"])(View, _objectSpread({}, this.props, {
                  items,
                  icons: _objectSpread({}, _icons__WEBPACK_IMPORTED_MODULE_2__, this.props.icons),
                  viewSize,
                  callbacks: {
                    changeView: this.changeView,
                    addItem: this.addItem,
                    updateItem: this.updateItem,
                    deleteItem: this.deleteItem,
                    changeModal: this.changeModal,
                    saveJSON: this.saveJSON
                  }
                }));
              }
            }]);
            return ReactStickyNotes2;
          }(react__WEBPACK_IMPORTED_MODULE_0__["Component"]);
          _defineProperty(ReactStickyNotes, "defaultProps", {
            useCSS: true,
            prefix: "rs-notes",
            colorCodes: Object(_utils__WEBPACK_IMPORTED_MODULE_3__["getColorCodes"])(),
            navbar: true,
            sessionKey: "react-sticky-notes",
            noteWidth: 220,
            noteHeight: 220,
            containerWidth: "100%",
            containerHeight: "100%",
            icons: _icons__WEBPACK_IMPORTED_MODULE_2__,
            useMaterialIcons: true
          });
          __webpack_exports__["default"] = ReactStickyNotes;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/index.scss": (
        /*!******************************************************!*\
          !*** ./src/components/react-sticky-notes/index.scss ***!
          \******************************************************/
        /*! no static exports found */
        /***/
        function(module2, exports2, __webpack_require__) {
          var content = __webpack_require__(
            /*! !../../../node_modules/css-loader!../../../node_modules/sass-loader/lib/loader.js!./index.scss */
            "./node_modules/css-loader/index.js!./node_modules/sass-loader/lib/loader.js!./src/components/react-sticky-notes/index.scss"
          );
          if (typeof content === "string") content = [[module2.i, content, ""]];
          var transform;
          var insertInto;
          var options = { "hmr": true };
          options.transform = transform;
          options.insertInto = void 0;
          var update = __webpack_require__(
            /*! ../../../node_modules/style-loader/lib/addStyles.js */
            "./node_modules/style-loader/lib/addStyles.js"
          )(content, options);
          if (content.locals) module2.exports = content.locals;
          if (false) {
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/modals/index.js": (
        /*!***********************************************************!*\
          !*** ./src/components/react-sticky-notes/modals/index.js ***!
          \***********************************************************/
        /*! exports provided: UploadModal */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _upload_modal__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./upload-modal */
            "./src/components/react-sticky-notes/modals/upload-modal.js"
          );
          __webpack_require__.d(__webpack_exports__, "UploadModal", function() {
            return _upload_modal__WEBPACK_IMPORTED_MODULE_0__["UploadModal"];
          });
        }
      ),
      /***/
      "./src/components/react-sticky-notes/modals/upload-modal.js": (
        /*!******************************************************************!*\
          !*** ./src/components/react-sticky-notes/modals/upload-modal.js ***!
          \******************************************************************/
        /*! exports provided: UploadModal */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "UploadModal", function() {
            return UploadModal;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var react__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_1___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_1__);
          function _typeof(obj) {
            if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
              _typeof = function _typeof2(obj2) {
                return typeof obj2;
              };
            } else {
              _typeof = function _typeof2(obj2) {
                return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
              };
            }
            return _typeof(obj);
          }
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _possibleConstructorReturn(self, call) {
            if (call && (_typeof(call) === "object" || typeof call === "function")) {
              return call;
            }
            return _assertThisInitialized(self);
          }
          function _getPrototypeOf(o) {
            _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf2(o2) {
              return o2.__proto__ || Object.getPrototypeOf(o2);
            };
            return _getPrototypeOf(o);
          }
          function _assertThisInitialized(self) {
            if (self === void 0) {
              throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
            }
            return self;
          }
          function _inherits(subClass, superClass) {
            if (typeof superClass !== "function" && superClass !== null) {
              throw new TypeError("Super expression must either be null or a function");
            }
            subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } });
            if (superClass) _setPrototypeOf(subClass, superClass);
          }
          function _setPrototypeOf(o, p) {
            _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf2(o2, p2) {
              o2.__proto__ = p2;
              return o2;
            };
            return _setPrototypeOf(o, p);
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          var UploadModal = function(_Component) {
            _inherits(UploadModal2, _Component);
            function UploadModal2() {
              var _this;
              _classCallCheck(this, UploadModal2);
              _this = _possibleConstructorReturn(this, _getPrototypeOf(UploadModal2).call(this));
              _defineProperty(_assertThisInitialized(_this), "uploadFile", function(e) {
                var file = e.target.files[0];
                if (file) {
                  if (file.type === "application/json" || file.type === "application/vnd.ms-excel") {
                    var reader = new FileReader();
                    reader.onload = function(readerEvent) {
                      var response, responseText;
                      switch (file.type) {
                        case "application/vnd.ms-excel":
                          response = Object(_utils__WEBPACK_IMPORTED_MODULE_0__["parseCSV"])(readerEvent.target.result);
                          break;
                        case "application/json":
                          response = JSON.parse(readerEvent.target.result);
                          break;
                      }
                      responseText = JSON.stringify(response, null, 4);
                      _this.handleResponse(null, responseText, response);
                    };
                    reader.onerror = function(readerEvent) {
                      this.handleResponse("File could not be read! Code " + readerEvent.target.error.code);
                    };
                    reader.readAsText(file);
                  } else {
                    _this.handleResponse("File type is not allowed. Please upload a JSON or CSV file.");
                  }
                }
              });
              _defineProperty(_assertThisInitialized(_this), "saveJSON", function(e) {
                var response = _this.state.response;
                if (response) {
                  _this.props.callbacks.saveJSON(e, response);
                }
              });
              _this.state = {
                error: "",
                response: null,
                contents: null
              };
              _this.jsonInput = react__WEBPACK_IMPORTED_MODULE_1___default.a.createRef();
              return _this;
            }
            _createClass(UploadModal2, [{
              key: "componentDidMount",
              value: function componentDidMount() {
                this.handleResponse(null, null, null);
              }
            }, {
              key: "handleResponse",
              value: function handleResponse(err, contents, response) {
                var error = err;
                if (response) {
                  if (!Array.isArray(response)) {
                    error = "Please upload a valid JSON or CSV file.";
                    response = null;
                    contents = null;
                  }
                }
                this.setState({
                  error,
                  contents,
                  response
                });
              }
            }, {
              key: "render",
              value: function render() {
                var _this2 = this;
                var _this$state = this.state, error = _this$state.error, contents = _this$state.contents;
                var props = this.props;
                return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
                  key: "".concat(props.prefix),
                  className: "".concat(props.prefix, " ").concat(props.prefix, "--file-upload")
                }, [contents ? Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("textarea", {
                  key: "file-upload--contents",
                  className: "".concat(props.prefix, "--file-preview"),
                  readOnly: true,
                  defaultValue: contents
                }) : null, Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
                  key: "".concat(props.prefix, "--file-drop"),
                  className: "".concat(props.prefix, "--file-drop ").concat(!contents ? props.prefix + "--file-drop__cover" : "")
                }, [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("input", {
                  key: "upload-button",
                  type: "file",
                  id: "".concat(props.prefix, "--file-input"),
                  className: "".concat(props.prefix, "--file-input"),
                  accept: ".json,.csv",
                  onChange: function onChange(e) {
                    return _this2.uploadFile(e);
                  },
                  placeholder: "upload file"
                }), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("p", {
                  key: "upload-link",
                  className: "".concat(props.prefix, "--upload-link")
                }, [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("label", {
                  key: "choose-a-file",
                  className: "".concat(props.prefix, "--file-label"),
                  htmlFor: "".concat(props.prefix, "--file-input")
                }, contents ? "Choose a another file" : "Choose a file"), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("span", {
                  key: "drop-a-file"
                }, " or drag it here.")])]), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
                  key: "file-upload--actions",
                  className: "".concat(props.prefix, "--upload-actions")
                }, [error ? Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("p", {
                  key: "upload-error",
                  className: "".concat(props.prefix, "--upload-error")
                }, error) : null, contents ? Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(react__WEBPACK_IMPORTED_MODULE_1__["Fragment"], {
                  key: "file-upload--save-cancel"
                }, Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
                  key: "file-upload--cancel",
                  className: "".concat(props.prefix, "--form-cancel"),
                  onClick: function onClick(e) {
                    return props.callbacks.changeModal(e, null);
                  }
                }, "Cancel"), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
                  key: "file-upload--save",
                  className: "".concat(props.prefix, "--form-save"),
                  onClick: this.saveJSON
                }, "Save")) : Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
                  key: "file-upload--cancel",
                  className: "".concat(props.prefix, "--form-cancel"),
                  onClick: function onClick(e) {
                    return props.callbacks.changeModal(e, null);
                  }
                }, "Back to notes.")])]);
              }
            }]);
            return UploadModal2;
          }(react__WEBPACK_IMPORTED_MODULE_1__["Component"]);
        }
      ),
      /***/
      "./src/components/react-sticky-notes/navbar/index.js": (
        /*!***********************************************************!*\
          !*** ./src/components/react-sticky-notes/navbar/index.js ***!
          \***********************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _partials_note_header__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../partials/note-header */
            "./src/components/react-sticky-notes/partials/note-header.js"
          );
          var _buttons__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./../buttons */
            "./src/components/react-sticky-notes/buttons/index.js"
          );
          function NavBar(_ref) {
            var viewSize = _ref.viewSize, prefix = _ref.prefix, items = _ref.items, callbacks = _ref.callbacks, icons = _ref.icons;
            var buttons = [_buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonTitle"], _buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonTrash"]];
            if (viewSize === "pageview" || viewSize === "fullscreen") {
              buttons.splice(1, 0, _buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonMenu"]);
            }
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              className: "".concat(prefix, "--navbar"),
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("navbar")
            }, [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: "".concat(prefix, "--navbar__nav"),
              className: "".concat(prefix, "--navbar__nav"),
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("navbar-nav", null, {
                flexGrow: 1
              })
            }, items ? items.map(function(data) {
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note_header__WEBPACK_IMPORTED_MODULE_1__["default"], {
                key: "navbar-item__".concat(data.id),
                data,
                prefix: "".concat(prefix, "--navbar__item"),
                icons,
                callbacks,
                buttons
              });
            }) : null), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: "navbar-item__options",
              className: "".concat(prefix, "--navbar__nav")
            }, Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note_header__WEBPACK_IMPORTED_MODULE_1__["default"], {
              prefix: "".concat(prefix, "--navbar__item"),
              viewSize,
              icons,
              callbacks,
              buttons: [_buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonAdd"], _buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonPageView"], _buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonUpload"], _buttons__WEBPACK_IMPORTED_MODULE_2__["ButtonTrash"]]
            }))]);
          }
          __webpack_exports__["default"] = NavBar;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-body.js": (
        /*!*****************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-body.js ***!
          \*****************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "default", function() {
            return NoteBody;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _note_text__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./note-text */
            "./src/components/react-sticky-notes/partials/note-text.js"
          );
          var _note_menu__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./note-menu */
            "./src/components/react-sticky-notes/partials/note-menu.js"
          );
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function NoteBody(props) {
            var data = props.data, prefix = props.prefix, callbacks = props.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              className: "".concat(prefix, "--note__body"),
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note-body", props)
            }, data.menu ? Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_note_menu__WEBPACK_IMPORTED_MODULE_2__["default"], _objectSpread({
              key: "note-menu"
            }, props)) : Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_note_text__WEBPACK_IMPORTED_MODULE_1__["default"], _objectSpread({
              key: "note-text"
            }, props)));
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-bubble.js": (
        /*!*******************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-bubble.js ***!
          \*******************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "default", function() {
            return NoteBubble;
          });
          var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_0___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
          var _utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _note_draggable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./note-draggable */
            "./src/components/react-sticky-notes/partials/note-draggable.js"
          );
          function _typeof(obj) {
            if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
              _typeof = function _typeof2(obj2) {
                return typeof obj2;
              };
            } else {
              _typeof = function _typeof2(obj2) {
                return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
              };
            }
            return _typeof(obj);
          }
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _possibleConstructorReturn(self, call) {
            if (call && (_typeof(call) === "object" || typeof call === "function")) {
              return call;
            }
            return _assertThisInitialized(self);
          }
          function _assertThisInitialized(self) {
            if (self === void 0) {
              throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
            }
            return self;
          }
          function _getPrototypeOf(o) {
            _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf2(o2) {
              return o2.__proto__ || Object.getPrototypeOf(o2);
            };
            return _getPrototypeOf(o);
          }
          function _inherits(subClass, superClass) {
            if (typeof superClass !== "function" && superClass !== null) {
              throw new TypeError("Super expression must either be null or a function");
            }
            subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } });
            if (superClass) _setPrototypeOf(subClass, superClass);
          }
          function _setPrototypeOf(o, p) {
            _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf2(o2, p2) {
              o2.__proto__ = p2;
              return o2;
            };
            return _setPrototypeOf(o, p);
          }
          var NoteBubble = function(_React$Component) {
            _inherits(NoteBubble2, _React$Component);
            function NoteBubble2(props) {
              var _this;
              _classCallCheck(this, NoteBubble2);
              _this = _possibleConstructorReturn(this, _getPrototypeOf(NoteBubble2).call(this, props));
              _this.targetRef = react__WEBPACK_IMPORTED_MODULE_0___default.a.createRef();
              return _this;
            }
            _createClass(NoteBubble2, [{
              key: "render",
              value: function render() {
                var props = this.props;
                return Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])(_note_draggable__WEBPACK_IMPORTED_MODULE_2__["default"], {
                  className: "".concat(props.prefix, "--note ").concat(props.data.selected ? props.prefix + "--note__selected" : ""),
                  position: props.data.position,
                  selected: props.data.selected,
                  target: this.targetRef,
                  onDragComplete: function onDragComplete(pos) {
                    return props.callbacks.updateItem(null, {
                      id: props.data.id,
                      position: pos
                    });
                  },
                  style: Object(_utils__WEBPACK_IMPORTED_MODULE_1__["getElementStyle"])("note", props)
                }, Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])("button", {
                  className: "".concat(props.prefix, "--note__bubble"),
                  ref: this.targetRef,
                  title: props.data.title ? props.data.title : Object(_utils__WEBPACK_IMPORTED_MODULE_1__["getNoteTitle"])(props.data),
                  onClick: function onClick() {
                    return props.callbacks.updateItem(null, {
                      id: props.data.id,
                      hidden: false
                    });
                  },
                  style: {
                    "--background-color": props.data.color,
                    width: "15px",
                    height: "15px",
                    borderRadius: "50%",
                    backgroundColor: props.data.color,
                    boxShadow: "1px 1px 2px rgba(0,0,0,.15)"
                  }
                }));
              }
            }]);
            return NoteBubble2;
          }(react__WEBPACK_IMPORTED_MODULE_0___default.a.Component);
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-dot.js": (
        /*!****************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-dot.js ***!
          \****************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "default", function() {
            return NoteDot;
          });
          var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_0___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
          var _utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _note_draggable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./note-draggable */
            "./src/components/react-sticky-notes/partials/note-draggable.js"
          );
          function _typeof(obj) {
            if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
              _typeof = function _typeof2(obj2) {
                return typeof obj2;
              };
            } else {
              _typeof = function _typeof2(obj2) {
                return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
              };
            }
            return _typeof(obj);
          }
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _possibleConstructorReturn(self, call) {
            if (call && (_typeof(call) === "object" || typeof call === "function")) {
              return call;
            }
            return _assertThisInitialized(self);
          }
          function _assertThisInitialized(self) {
            if (self === void 0) {
              throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
            }
            return self;
          }
          function _getPrototypeOf(o) {
            _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf2(o2) {
              return o2.__proto__ || Object.getPrototypeOf(o2);
            };
            return _getPrototypeOf(o);
          }
          function _inherits(subClass, superClass) {
            if (typeof superClass !== "function" && superClass !== null) {
              throw new TypeError("Super expression must either be null or a function");
            }
            subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } });
            if (superClass) _setPrototypeOf(subClass, superClass);
          }
          function _setPrototypeOf(o, p) {
            _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf2(o2, p2) {
              o2.__proto__ = p2;
              return o2;
            };
            return _setPrototypeOf(o, p);
          }
          var NoteDot = function(_React$Component) {
            _inherits(NoteDot2, _React$Component);
            function NoteDot2(props) {
              var _this;
              _classCallCheck(this, NoteDot2);
              _this = _possibleConstructorReturn(this, _getPrototypeOf(NoteDot2).call(this, props));
              _this.targetRef = react__WEBPACK_IMPORTED_MODULE_0___default.a.createRef();
              return _this;
            }
            _createClass(NoteDot2, [{
              key: "render",
              value: function render() {
                var props = this.props;
                return Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])(_note_draggable__WEBPACK_IMPORTED_MODULE_2__["default"], {
                  unit: "%",
                  useBoundaries: true,
                  disabledAxisX: true,
                  className: "".concat(props.prefix, "--note ").concat(props.data.selected ? props.prefix + "--note__selected" : ""),
                  position: props.data.position,
                  selected: props.data.selected,
                  target: this.targetRef,
                  onDragComplete: function onDragComplete(pos) {
                    var index = Math.floor(pos.py * props.colorCodes.length / 100);
                    var color = props.colorCodes[index];
                    props.callbacks.updateItem(null, {
                      id: props.data.id,
                      color
                    });
                  },
                  style: {
                    position: "absolute",
                    left: props.data.position.x,
                    top: props.data.position.y
                  }
                }, Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])("button", {
                  className: "".concat(props.prefix, "--note__bubble"),
                  ref: this.targetRef,
                  title: props.data.title ? props.data.title : Object(_utils__WEBPACK_IMPORTED_MODULE_1__["getNoteTitle"])(props.data),
                  onClick: function onClick() {
                    return props.callbacks.updateItem(null, {
                      id: props.data.id,
                      hidden: false
                    });
                  },
                  style: {
                    "--background-color": props.data.color,
                    width: "15px",
                    height: "15px",
                    borderRadius: "50%",
                    backgroundColor: props.data.color,
                    boxShadow: "1px 1px 2px rgba(0,0,0,.15)"
                  }
                }));
              }
            }]);
            return NoteDot2;
          }(react__WEBPACK_IMPORTED_MODULE_0___default.a.Component);
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-draggable.js": (
        /*!**********************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-draggable.js ***!
          \**********************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_0___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
          var _utils_draggable__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../utils/draggable */
            "./src/components/react-sticky-notes/utils/draggable.js"
          );
          var _utils__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          function _typeof(obj) {
            if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
              _typeof = function _typeof2(obj2) {
                return typeof obj2;
              };
            } else {
              _typeof = function _typeof2(obj2) {
                return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
              };
            }
            return _typeof(obj);
          }
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _possibleConstructorReturn(self, call) {
            if (call && (_typeof(call) === "object" || typeof call === "function")) {
              return call;
            }
            return _assertThisInitialized(self);
          }
          function _getPrototypeOf(o) {
            _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf2(o2) {
              return o2.__proto__ || Object.getPrototypeOf(o2);
            };
            return _getPrototypeOf(o);
          }
          function _assertThisInitialized(self) {
            if (self === void 0) {
              throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
            }
            return self;
          }
          function _inherits(subClass, superClass) {
            if (typeof superClass !== "function" && superClass !== null) {
              throw new TypeError("Super expression must either be null or a function");
            }
            subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } });
            if (superClass) _setPrototypeOf(subClass, superClass);
          }
          function _setPrototypeOf(o, p) {
            _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf2(o2, p2) {
              o2.__proto__ = p2;
              return o2;
            };
            return _setPrototypeOf(o, p);
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          var NoteDraggable = function(_React$Component) {
            _inherits(NoteDraggable2, _React$Component);
            function NoteDraggable2(props) {
              var _this;
              _classCallCheck(this, NoteDraggable2);
              _this = _possibleConstructorReturn(this, _getPrototypeOf(NoteDraggable2).call(this, props));
              _defineProperty(_assertThisInitialized(_this), "draggable", null);
              _defineProperty(_assertThisInitialized(_this), "onMouseDown", function(e) {
                if (_this.props.target && e.target === _this.props.target.current) {
                  _this.draggable.onMouseDown(e);
                }
              });
              _this.state = {
                options: {}
              };
              _this.element = react__WEBPACK_IMPORTED_MODULE_0___default.a.createRef();
              _this.draggable = new _utils_draggable__WEBPACK_IMPORTED_MODULE_1__["default"]();
              return _this;
            }
            _createClass(NoteDraggable2, [{
              key: "componentDidMount",
              value: function componentDidMount() {
                var _this2 = this;
                var el = this.element ? this.element.current : null;
                var options = {
                  element: el,
                  unit: this.props.unit,
                  useBoundaries: this.props.useBoundaries,
                  disabledAxisX: this.props.disabledAxisX,
                  position: this.props.position,
                  onDragComplete: this.props.onDragComplete,
                  onInit: this.props.onInit
                };
                this.setState({
                  options
                }, function() {
                  _this2.draggable.init(options);
                });
              }
            }, {
              key: "render",
              value: function render() {
                return Object(_utils__WEBPACK_IMPORTED_MODULE_2__["h"])("div", {
                  className: this.props.className,
                  style: this.props.style,
                  ref: this.element,
                  onMouseDown: this.onMouseDown,
                  onTouchStart: this.onMouseDown
                }, this.props.children);
              }
            }]);
            return NoteDraggable2;
          }(react__WEBPACK_IMPORTED_MODULE_0___default.a.Component);
          __webpack_exports__["default"] = NoteDraggable;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-header.js": (
        /*!*******************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-header.js ***!
          \*******************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function NoteHeader(props) {
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              className: props.prefix,
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note-header", {
                data: props.data
              })
            }, props.buttons ? props.buttons.map(function(Button, i) {
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(Button, _objectSpread({
                key: "".concat(props.prefix).concat(props.data ? props.data.id : "all", "__note-button__").concat(i)
              }, props));
            }) : null);
          }
          __webpack_exports__["default"] = NoteHeader;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-menu.js": (
        /*!*****************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-menu.js ***!
          \*****************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          function NoteMenu(props) {
            var data = props.data, index = props.index, prefix = props.prefix, colorCodes = props.colorCodes, callbacks = props.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              className: "".concat(prefix, "--colors"),
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note-menu", props)
            }, colorCodes.map(function(colorCode) {
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("button", {
                key: colorCode,
                onClick: function onClick(e) {
                  return callbacks.updateItem(e, {
                    id: data.id,
                    color: colorCode,
                    menu: false
                  });
                },
                className: "".concat(prefix, "--colors__color ").concat(data.color === colorCode ? prefix + "--colors__color--selected" : ""),
                style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note-color-selector", {
                  colorCode
                })
              }, colorCode);
            }));
          }
          __webpack_exports__["default"] = NoteMenu;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note-text.js": (
        /*!*****************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note-text.js ***!
          \*****************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          function NoteText(_ref) {
            var data = _ref.data, index = _ref.index, prefix = _ref.prefix, callbacks = _ref.callbacks;
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              className: "".concat(prefix, "--text"),
              placeholder: "react-hooks",
              contentEditable: "true",
              onBlur: function onBlur(e) {
                return callbacks.updateItem(index, {
                  id: data.id,
                  text: e.target.innerText
                });
              },
              onFocus: function onFocus(e) {
                return callbacks.updateItem(e, {
                  id: data.id,
                  selected: true,
                  datetime: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getCurrentDateTime"])()
                });
              },
              dangerouslySetInnerHTML: {
                __html: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["nlToBr"])(data.text)
              },
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note-input")
            });
          }
          __webpack_exports__["default"] = NoteText;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/partials/note.js": (
        /*!************************************************************!*\
          !*** ./src/components/react-sticky-notes/partials/note.js ***!
          \************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_0___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
          var _utils__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _note_draggable__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./note-draggable */
            "./src/components/react-sticky-notes/partials/note-draggable.js"
          );
          var _note_header__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
            /*! ./note-header */
            "./src/components/react-sticky-notes/partials/note-header.js"
          );
          var _note_body__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
            /*! ./note-body */
            "./src/components/react-sticky-notes/partials/note-body.js"
          );
          var _buttons__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
            /*! ./../buttons */
            "./src/components/react-sticky-notes/buttons/index.js"
          );
          function _typeof(obj) {
            if (typeof Symbol === "function" && typeof Symbol.iterator === "symbol") {
              _typeof = function _typeof2(obj2) {
                return typeof obj2;
              };
            } else {
              _typeof = function _typeof2(obj2) {
                return obj2 && typeof Symbol === "function" && obj2.constructor === Symbol && obj2 !== Symbol.prototype ? "symbol" : typeof obj2;
              };
            }
            return _typeof(obj);
          }
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _possibleConstructorReturn(self, call) {
            if (call && (_typeof(call) === "object" || typeof call === "function")) {
              return call;
            }
            return _assertThisInitialized(self);
          }
          function _assertThisInitialized(self) {
            if (self === void 0) {
              throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
            }
            return self;
          }
          function _getPrototypeOf(o) {
            _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf2(o2) {
              return o2.__proto__ || Object.getPrototypeOf(o2);
            };
            return _getPrototypeOf(o);
          }
          function _inherits(subClass, superClass) {
            if (typeof superClass !== "function" && superClass !== null) {
              throw new TypeError("Super expression must either be null or a function");
            }
            subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, writable: true, configurable: true } });
            if (superClass) _setPrototypeOf(subClass, superClass);
          }
          function _setPrototypeOf(o, p) {
            _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf2(o2, p2) {
              o2.__proto__ = p2;
              return o2;
            };
            return _setPrototypeOf(o, p);
          }
          var Note = function(_React$Component) {
            _inherits(Note2, _React$Component);
            function Note2(props) {
              var _this;
              _classCallCheck(this, Note2);
              _this = _possibleConstructorReturn(this, _getPrototypeOf(Note2).call(this, props));
              _this.targetRef = react__WEBPACK_IMPORTED_MODULE_0___default.a.createRef();
              return _this;
            }
            _createClass(Note2, [{
              key: "render",
              value: function render() {
                var props = this.props;
                return Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])(_note_draggable__WEBPACK_IMPORTED_MODULE_2__["default"], {
                  className: "".concat(props.prefix, "--note ").concat(props.data.selected ? props.prefix + "--note__selected" : ""),
                  position: props.data.position,
                  selected: props.data.selected,
                  target: this.targetRef,
                  onDragComplete: function onDragComplete(pos) {
                    return props.callbacks.updateItem(null, {
                      id: props.data.id,
                      position: pos
                    });
                  },
                  style: Object(_utils__WEBPACK_IMPORTED_MODULE_1__["getElementStyle"])("note", props, {
                    boxShadow: "1px 1px 2px rgba(0,0,0,.15)"
                  })
                }, [Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])(_note_header__WEBPACK_IMPORTED_MODULE_3__["default"], _objectSpread({}, props, {
                  key: "note-header",
                  targetRef: this.targetRef,
                  prefix: "".concat(props.prefix, "--header"),
                  buttons: [_buttons__WEBPACK_IMPORTED_MODULE_5__["ButtonAdd"], _buttons__WEBPACK_IMPORTED_MODULE_5__["ButtonTitle"], _buttons__WEBPACK_IMPORTED_MODULE_5__["ButtonMenu"], _buttons__WEBPACK_IMPORTED_MODULE_5__["ButtonHideShow"], _buttons__WEBPACK_IMPORTED_MODULE_5__["ButtonTrash"]]
                })), Object(_utils__WEBPACK_IMPORTED_MODULE_1__["h"])(_note_body__WEBPACK_IMPORTED_MODULE_4__["default"], _objectSpread({
                  key: "note-body"
                }, props))]);
              }
            }]);
            return Note2;
          }(react__WEBPACK_IMPORTED_MODULE_0___default.a.Component);
          __webpack_exports__["default"] = Note;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/reducers/reducer.js": (
        /*!***************************************************************!*\
          !*** ./src/components/react-sticky-notes/reducers/reducer.js ***!
          \***************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          var reducer = function reducer2(state, action) {
            var viewSizes = ["bubbleview", "normalview", "pageview", "fullscreen"];
            var params = action.payload && action.payload.data ? Object.keys(action.payload.data) : [];
            var items = state.items, viewSize = state.viewSize, modal = state.modal;
            switch (action.type) {
              case "changemodal":
                modal = action.payload.modal;
                break;
              case "import":
                modal = null;
                items = action.payload.items;
                break;
              case "changeview":
                modal = null;
                var currentViewSize = viewSizes.indexOf(viewSize);
                viewSize = currentViewSize > -1 && currentViewSize < viewSizes.length - 1 ? viewSizes[currentViewSize + 1] : viewSizes[0];
                break;
              case "add":
                items = items.map(function(item) {
                  item.selected = false;
                  return item;
                });
                items.splice(action.payload.index, 0, action.payload.data);
                break;
              case "update":
                items = items.map(function(item) {
                  if (item.id === action.payload.data.id) {
                    item = _objectSpread({}, item, action.payload.data);
                  }
                  if (params.indexOf("selected") !== -1) {
                    item.selected = item.id === action.payload.data.id ? action.payload.data.selected : false;
                  }
                  if (params.indexOf("menu") !== -1) {
                    item.menu = item.id === action.payload.data.id ? action.payload.data.menu : false;
                  }
                  return item;
                });
                break;
              case "delete":
                var index = items.findIndex(function(item) {
                  return action.payload.data.id === item.id;
                });
                if (index !== -1) {
                  items.splice(index, 1);
                } else {
                  items.splice(0, items.length);
                }
                break;
              default:
                items = state.items;
                break;
            }
            return {
              items,
              viewSize,
              modal
            };
          };
          __webpack_exports__["default"] = reducer;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/color-codes.js": (
        /*!****************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/color-codes.js ***!
          \****************************************************************/
        /*! exports provided: getColorCodes */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "getColorCodes", function() {
            return getColorCodes;
          });
          function getColorCodes() {
            var codes = [];
            for (var i = 0; i < 360; i += 18) {
              codes.push("hsl(".concat(i, ",50%, 50%)"));
            }
            return codes;
          }
          ;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/draggable.js": (
        /*!**************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/draggable.js ***!
          \**************************************************************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "default", function() {
            return Draggable;
          });
          function _classCallCheck(instance, Constructor) {
            if (!(instance instanceof Constructor)) {
              throw new TypeError("Cannot call a class as a function");
            }
          }
          function _defineProperties(target, props) {
            for (var i = 0; i < props.length; i++) {
              var descriptor = props[i];
              descriptor.enumerable = descriptor.enumerable || false;
              descriptor.configurable = true;
              if ("value" in descriptor) descriptor.writable = true;
              Object.defineProperty(target, descriptor.key, descriptor);
            }
          }
          function _createClass(Constructor, protoProps, staticProps) {
            if (protoProps) _defineProperties(Constructor.prototype, protoProps);
            if (staticProps) _defineProperties(Constructor, staticProps);
            return Constructor;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          var Draggable = function() {
            function Draggable2() {
              var _this = this;
              _classCallCheck(this, Draggable2);
              _defineProperty(this, "dx", 0);
              _defineProperty(this, "dy", 0);
              _defineProperty(this, "percentX", 0);
              _defineProperty(this, "percentY", 0);
              _defineProperty(this, "currentX", 0);
              _defineProperty(this, "currentY", 0);
              _defineProperty(this, "onMouseMove", function(e) {
                if (e.cancelable) {
                  e.preventDefault();
                }
                var el = _this.options.element;
                var parentElement = el.parentElement;
                var pRect = parentElement ? parentElement.getBoundingClientRect() : {
                  left: 0,
                  top: 0
                };
                var position = _this.getPosition(e, _this.dx, _this.dy);
                var x = position.left - pRect.left;
                var y = position.top - pRect.top;
                _this.currentX = x > 0 ? x : 0;
                _this.currentY = y > 0 ? y : 0;
                if (_this.options.useBoundaries) {
                  var maxX = pRect.width - el.offsetWidth;
                  var maxY = pRect.height - el.offsetHeight;
                  if (_this.currentX >= maxX) {
                    _this.currentX = maxX;
                  }
                  if (_this.currentY >= maxY) {
                    _this.currentY = maxY;
                  }
                }
                if (_this.options.unit === "%") {
                  _this.percentX = _this.currentX * 100 / pRect.width;
                  _this.percentY = _this.currentY * 100 / pRect.height;
                  _this.setTranslate("".concat(_this.percentX, "%"), "".concat(_this.percentY, "%"));
                } else {
                  _this.setTranslate("".concat(_this.currentX, "px"), "".concat(_this.currentY, "px"));
                }
              });
              _defineProperty(this, "onMouseDown", function(e) {
                var el = _this.options.element;
                var parentElement = el.parentElement;
                var rect = el.getBoundingClientRect();
                var pRect = parentElement ? parentElement.getBoundingClientRect() : {
                  left: 0,
                  top: 0
                };
                _this.currentX = -pRect.left + rect.left;
                _this.currentY = -pRect.top + rect.top;
                var position = _this.getPosition(e);
                _this.dx = position.left - rect.left;
                _this.dy = position.top - rect.top;
                el.classList.add("draggable");
                document.addEventListener("mousemove", _this.onMouseMove, null);
                document.addEventListener("mouseup", _this.onMouseUp, null);
                document.addEventListener("touchmove", _this.onMouseMove, {
                  passive: false
                });
                document.addEventListener("touchend", _this.onMouseUp, {
                  passive: false
                });
              });
              _defineProperty(this, "onMouseUp", function(e) {
                if (_this.options.onDragComplete) {
                  _this.options.onDragComplete.call(_this, {
                    x: _this.currentX,
                    y: _this.currentY,
                    px: _this.percentX,
                    py: _this.percentY
                  });
                }
                _this.options.element.classList.remove("draggable");
                document.removeEventListener("mousemove", _this.onMouseMove);
                document.removeEventListener("mouseup", _this.onMouseUp);
                document.removeEventListener("touchmove", _this.onMouseMove);
                document.removeEventListener("touchend", _this.onMouseUp);
              });
            }
            _createClass(Draggable2, [{
              key: "init",
              value: function init(options) {
                this.options = options;
              }
            }, {
              key: "setTranslate",
              value: function setTranslate(x, y) {
                if (this.options.element) {
                  if (!this.options.disabledAxisX) {
                    this.options.element.style.left = x;
                  }
                  if (!this.options.disabledAxisY) {
                    this.options.element.style.top = y;
                  }
                }
              }
            }, {
              key: "getPosition",
              value: function getPosition(e) {
                var dx = arguments.length > 1 && arguments[1] !== void 0 ? arguments[1] : 0;
                var dy = arguments.length > 2 && arguments[2] !== void 0 ? arguments[2] : 0;
                if (/touch/.test(e.type)) {
                  return {
                    left: e.touches[0].clientX - dx,
                    top: e.touches[0].clientY - dy,
                    x: e.touches[0].clientX - dx,
                    y: e.touches[0].clientY - dy
                  };
                } else {
                  return {
                    left: e.clientX - dx,
                    top: e.clientY - dy,
                    x: e.clientX - dx,
                    y: e.clientY - dy
                  };
                }
              }
            }]);
            return Draggable2;
          }();
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/get-current-datetime.js": (
        /*!*************************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/get-current-datetime.js ***!
          \*************************************************************************/
        /*! exports provided: getCurrentDateTime */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "getCurrentDateTime", function() {
            return getCurrentDateTime;
          });
          function getCurrentDateTime() {
            return (/* @__PURE__ */ new Date()).toISOString().replace("T", " ").substring(0, 19);
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/get-element-style.js": (
        /*!**********************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/get-element-style.js ***!
          \**********************************************************************/
        /*! exports provided: getElementStyle */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "getElementStyle", function() {
            return getElementStyle;
          });
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function getElementStyle(nodeName, props) {
            var defaultStyle = arguments.length > 2 && arguments[2] !== void 0 ? arguments[2] : {};
            var style = defaultStyle;
            switch (nodeName) {
              case "container":
                style = _objectSpread({}, defaultStyle, {
                  position: "relative",
                  width: props.containerWidth,
                  height: props.containerHeight,
                  backgroundColor: props.backgroundColor
                });
                break;
              case "note":
                style = _objectSpread({}, defaultStyle, {
                  position: "absolute",
                  left: props.viewSize === "pageview" || props.viewSize === "fullscreen" ? 0 : props.data.position ? "".concat(props.data.position.x, "px") : 0,
                  top: props.viewSize === "pageview" || props.viewSize === "fullscreen" ? 0 : props.data.position ? "".concat(props.data.position.y, "px") : 0,
                  width: props.viewSize === "pageview" || props.viewSize === "fullscreen" ? "100%" : null,
                  height: props.viewSize === "pageview" || props.viewSize === "fullscreen" ? "100%" : null
                });
                if (props.data.selected) {
                  style.zIndex = 1;
                }
                break;
              case "note-body":
                style.width = props.viewSize === "pageview" || props.viewSize === "fullscreen" ? "100%" : props.noteWidth, style.height = props.viewSize === "pageview" || props.viewSize === "fullscreen" ? "100%" : props.noteHeight, style.backgroundColor = props.data.color, style.overflow = "auto";
                if (props.data.selected) {
                  style.minWidth = props.noteWidth, style.resize = "both";
                }
                break;
              case "note-input":
                style.height = "100%";
                break;
              case "note-header":
                style.backgroundColor = props.data ? props.data.color : "";
                break;
              case "note-minimized":
                style = _objectSpread({}, defaultStyle, {
                  backgroundColor: props.data.color,
                  position: "absolute",
                  left: props.data.position ? "".concat(props.data.position.x, "px") : 0,
                  top: props.data.position ? "".concat(props.data.position.y, "px") : 0,
                  width: "10px",
                  height: "10px"
                });
                break;
              case "note-maximized":
                style = _objectSpread({}, defaultStyle, {
                  backgroundColor: props.data.color,
                  position: "absolute",
                  left: props.data.position ? "".concat(props.data.position.x, "px") : 0,
                  top: props.data.position ? "".concat(props.data.position.y, "px") : 0,
                  width: "10px",
                  height: "10px"
                });
                break;
              case "note-menu":
                style.backgroundColor = "#ffffff";
                style.minHeight = "100%";
                break;
              case "note-color-selector":
                style = _objectSpread({}, defaultStyle, {
                  backgroundColor: props.colorCode
                });
                break;
              case "icon":
                style = _objectSpread({}, defaultStyle, {
                  verticalAlign: "middle",
                  width: "1em"
                });
                break;
            }
            return style;
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/get-note-title.js": (
        /*!*******************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/get-note-title.js ***!
          \*******************************************************************/
        /*! exports provided: getNoteTitle */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "getNoteTitle", function() {
            return getNoteTitle;
          });
          function getNoteTitle(_ref) {
            var title = _ref.title, text = _ref.text, _ref$limit = _ref.limit, limit = _ref$limit === void 0 ? 10 : _ref$limit, _ref$delimiter = _ref.delimiter, delimiter = _ref$delimiter === void 0 ? null : _ref$delimiter;
            var _title;
            if (title) {
              _title = String(title);
            } else if (delimiter) {
              _title = String(text).split(delimiter)[0];
            } else {
              _title = String(text).substr(0, limit);
            }
            return _title.substr(0, 1).toUpperCase() + _title.substr(1, _title.length);
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/get-notes.js": (
        /*!**************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/get-notes.js ***!
          \**************************************************************/
        /*! exports provided: getNotes */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "getNotes", function() {
            return getNotes;
          });
          var _get_uuid__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./get-uuid */
            "./src/components/react-sticky-notes/utils/get-uuid.js"
          );
          function getNotes(colorCodes, notes) {
            var _notes = [];
            if (notes) {
              _notes = notes.map(function(note) {
                note.id = note.id ? note.id : Object(_get_uuid__WEBPACK_IMPORTED_MODULE_0__["getUUID"])();
                note.position = note.position ? note.position : {
                  x: 0,
                  y: 0
                };
                note.color = note.color ? note.color : colorCodes[Math.floor(Math.random() * colorCodes.length)];
                return note;
              });
            } else if (localStorage.getItem("react-sticky-notes")) {
              _notes = JSON.parse(localStorage.getItem("react-sticky-notes"));
            } else {
              _notes = [{
                id: Object(_get_uuid__WEBPACK_IMPORTED_MODULE_0__["getUUID"])(),
                text: "",
                position: {
                  x: 0,
                  y: 0
                },
                color: colorCodes[Math.floor(Math.random() * colorCodes.length)],
                selected: true
              }];
            }
            return _notes;
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/get-uuid.js": (
        /*!*************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/get-uuid.js ***!
          \*************************************************************/
        /*! exports provided: getUUID */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "getUUID", function() {
            return getUUID;
          });
          function getUUID() {
            var dt = (/* @__PURE__ */ new Date()).getTime();
            var uuid = "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, function(c) {
              var r = (dt + Math.random() * 16) % 16 | 0;
              dt = Math.floor(dt / 16);
              return (c == "x" ? r : r & 3 | 8).toString(16);
            });
            return uuid;
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/h.js": (
        /*!******************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/h.js ***!
          \******************************************************/
        /*! exports provided: h */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "h", function() {
            return h;
          });
          var react__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! react */
            "react"
          );
          var react__WEBPACK_IMPORTED_MODULE_0___default = __webpack_require__.n(react__WEBPACK_IMPORTED_MODULE_0__);
          var h = react__WEBPACK_IMPORTED_MODULE_0___default.a.createElement;
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/index.js": (
        /*!**********************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/index.js ***!
          \**********************************************************/
        /*! exports provided: h, getColorCodes, getUUID, nlToBr, getNotes, getElementStyle, getCurrentDateTime, getNoteTitle, parseCSV */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _h__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./h */
            "./src/components/react-sticky-notes/utils/h.js"
          );
          __webpack_require__.d(__webpack_exports__, "h", function() {
            return _h__WEBPACK_IMPORTED_MODULE_0__["h"];
          });
          var _color_codes__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./color-codes */
            "./src/components/react-sticky-notes/utils/color-codes.js"
          );
          __webpack_require__.d(__webpack_exports__, "getColorCodes", function() {
            return _color_codes__WEBPACK_IMPORTED_MODULE_1__["getColorCodes"];
          });
          var _get_uuid__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./get-uuid */
            "./src/components/react-sticky-notes/utils/get-uuid.js"
          );
          __webpack_require__.d(__webpack_exports__, "getUUID", function() {
            return _get_uuid__WEBPACK_IMPORTED_MODULE_2__["getUUID"];
          });
          var _nl_to_br__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
            /*! ./nl-to-br */
            "./src/components/react-sticky-notes/utils/nl-to-br.js"
          );
          __webpack_require__.d(__webpack_exports__, "nlToBr", function() {
            return _nl_to_br__WEBPACK_IMPORTED_MODULE_3__["nlToBr"];
          });
          var _get_notes__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
            /*! ./get-notes */
            "./src/components/react-sticky-notes/utils/get-notes.js"
          );
          __webpack_require__.d(__webpack_exports__, "getNotes", function() {
            return _get_notes__WEBPACK_IMPORTED_MODULE_4__["getNotes"];
          });
          var _get_element_style__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
            /*! ./get-element-style */
            "./src/components/react-sticky-notes/utils/get-element-style.js"
          );
          __webpack_require__.d(__webpack_exports__, "getElementStyle", function() {
            return _get_element_style__WEBPACK_IMPORTED_MODULE_5__["getElementStyle"];
          });
          var _get_current_datetime__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
            /*! ./get-current-datetime */
            "./src/components/react-sticky-notes/utils/get-current-datetime.js"
          );
          __webpack_require__.d(__webpack_exports__, "getCurrentDateTime", function() {
            return _get_current_datetime__WEBPACK_IMPORTED_MODULE_6__["getCurrentDateTime"];
          });
          var _get_note_title__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
            /*! ./get-note-title */
            "./src/components/react-sticky-notes/utils/get-note-title.js"
          );
          __webpack_require__.d(__webpack_exports__, "getNoteTitle", function() {
            return _get_note_title__WEBPACK_IMPORTED_MODULE_7__["getNoteTitle"];
          });
          var _parse_csv__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(
            /*! ./parse-csv */
            "./src/components/react-sticky-notes/utils/parse-csv.js"
          );
          __webpack_require__.d(__webpack_exports__, "parseCSV", function() {
            return _parse_csv__WEBPACK_IMPORTED_MODULE_8__["parseCSV"];
          });
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/nl-to-br.js": (
        /*!*************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/nl-to-br.js ***!
          \*************************************************************/
        /*! exports provided: nlToBr */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "nlToBr", function() {
            return nlToBr;
          });
          function nlToBr(str) {
            return str ? str.replace(/(?:\r\n|\r|\n)/g, "<br>") : "";
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/utils/parse-csv.js": (
        /*!**************************************************************!*\
          !*** ./src/components/react-sticky-notes/utils/parse-csv.js ***!
          \**************************************************************/
        /*! exports provided: parseCSV */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "parseCSV", function() {
            return parseCSV;
          });
          function parseCSV(str) {
            var arr = [];
            var quote = false;
            for (var row = 0, col = 0, c = 0; c < str.length; c++) {
              var currentCharacter = str[c], nextCharacter = str[c + 1];
              arr[row] = arr[row] || [];
              arr[row][col] = arr[row][col] || "";
              if (currentCharacter == '"' && quote && nextCharacter == '"') {
                arr[row][col] += currentCharacter;
                ++c;
                continue;
              }
              if (currentCharacter == '"') {
                quote = !quote;
                continue;
              }
              if (currentCharacter == "," && !quote) {
                ++col;
                continue;
              }
              if (currentCharacter == "\r" && nextCharacter == "\n" && !quote) {
                col = 0;
                ++row;
                ++c;
                continue;
              }
              if ((currentCharacter == "\r" || currentCharacter == "\n") && !quote) {
                ++row;
                col = 0;
                continue;
              }
              arr[row][col] += currentCharacter;
            }
            var results = [];
            var headers = arr[0];
            for (var i = 1; i < arr.length; i++) {
              var result = {};
              for (var j = 0; j < arr[i].length; j++) {
                result[headers[j]] = arr[i][j];
              }
              results.push(result);
            }
            return results;
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/views/bubble-view.js": (
        /*!****************************************************************!*\
          !*** ./src/components/react-sticky-notes/views/bubble-view.js ***!
          \****************************************************************/
        /*! exports provided: BubbleView */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "BubbleView", function() {
            return BubbleView;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _navbar__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../navbar */
            "./src/components/react-sticky-notes/navbar/index.js"
          );
          var _partials_note_dot__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ../partials/note-dot */
            "./src/components/react-sticky-notes/partials/note-dot.js"
          );
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function BubbleView(props) {
            return [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_navbar__WEBPACK_IMPORTED_MODULE_1__["default"], _objectSpread({}, props, {
              key: "navbar"
            })), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: props.prefix,
              className: props.prefix,
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("container", props, {
                display: "flex"
              })
            }, [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: "".concat(props.prefix, "--notes-colors"),
              className: "".concat(props.prefix, "--notes-colors")
            }, props.colorCodes.map(function(color, i) {
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
                key: "note--color-".concat(i),
                className: "".concat(props.prefix, "--notes-colors__color"),
                style: {
                  "--background-color": color,
                  "--height": "".concat(100 / props.colorCodes.length, "%")
                }
              });
            })), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: "".concat(props.prefix, "--notes-area"),
              className: "".concat(props.prefix, "--notes-area")
            }, props.items.map(function(data, index) {
              data.position = {
                x: "".concat(index * 100 / props.items.length, "%"),
                y: "".concat(props.colorCodes.indexOf(data.color) * 100 / props.colorCodes.length, "%")
              };
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note_dot__WEBPACK_IMPORTED_MODULE_2__["default"], _objectSpread({
                key: "note-".concat(data.id)
              }, props, {
                data
              }));
            }))])];
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/views/fullscreen-view.js": (
        /*!********************************************************************!*\
          !*** ./src/components/react-sticky-notes/views/fullscreen-view.js ***!
          \********************************************************************/
        /*! exports provided: FullscreenView */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "FullscreenView", function() {
            return FullscreenView;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _navbar__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../navbar */
            "./src/components/react-sticky-notes/navbar/index.js"
          );
          var _partials_note_body__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ../partials/note-body */
            "./src/components/react-sticky-notes/partials/note-body.js"
          );
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function FullscreenView(props) {
            return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              style: {
                position: "fixed",
                left: 0,
                top: 0,
                width: "100vw",
                height: "100vh"
              }
            }, [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_navbar__WEBPACK_IMPORTED_MODULE_1__["default"], _objectSpread({}, props, {
              key: "navbar"
            })), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: props.prefix,
              className: props.prefix,
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("container", props)
            }, props.items.map(function(data) {
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
                key: "note-".concat(data.id),
                className: "".concat(props.prefix, "--note"),
                style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note", _objectSpread({}, props, {
                  data
                }))
              }, Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note_body__WEBPACK_IMPORTED_MODULE_2__["default"], _objectSpread({
                data
              }, props)));
            }))]);
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/views/index.js": (
        /*!**********************************************************!*\
          !*** ./src/components/react-sticky-notes/views/index.js ***!
          \**********************************************************/
        /*! exports provided: NormalView, BubbleView, PageView, FullscreenView */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _normal_view__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./normal-view */
            "./src/components/react-sticky-notes/views/normal-view.js"
          );
          __webpack_require__.d(__webpack_exports__, "NormalView", function() {
            return _normal_view__WEBPACK_IMPORTED_MODULE_0__["NormalView"];
          });
          var _bubble_view__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./bubble-view */
            "./src/components/react-sticky-notes/views/bubble-view.js"
          );
          __webpack_require__.d(__webpack_exports__, "BubbleView", function() {
            return _bubble_view__WEBPACK_IMPORTED_MODULE_1__["BubbleView"];
          });
          var _page_view__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ./page-view */
            "./src/components/react-sticky-notes/views/page-view.js"
          );
          __webpack_require__.d(__webpack_exports__, "PageView", function() {
            return _page_view__WEBPACK_IMPORTED_MODULE_2__["PageView"];
          });
          var _fullscreen_view__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
            /*! ./fullscreen-view */
            "./src/components/react-sticky-notes/views/fullscreen-view.js"
          );
          __webpack_require__.d(__webpack_exports__, "FullscreenView", function() {
            return _fullscreen_view__WEBPACK_IMPORTED_MODULE_3__["FullscreenView"];
          });
        }
      ),
      /***/
      "./src/components/react-sticky-notes/views/normal-view.js": (
        /*!****************************************************************!*\
          !*** ./src/components/react-sticky-notes/views/normal-view.js ***!
          \****************************************************************/
        /*! exports provided: NormalView */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "NormalView", function() {
            return NormalView;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _navbar__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../navbar */
            "./src/components/react-sticky-notes/navbar/index.js"
          );
          var _partials_note__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ../partials/note */
            "./src/components/react-sticky-notes/partials/note.js"
          );
          var _partials_note_bubble__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
            /*! ../partials/note-bubble */
            "./src/components/react-sticky-notes/partials/note-bubble.js"
          );
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function NormalView(props) {
            return [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_navbar__WEBPACK_IMPORTED_MODULE_1__["default"], _objectSpread({}, props, {
              key: "navbar"
            })), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: props.prefix,
              className: props.prefix,
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("container", props)
            }, props.items.map(function(data) {
              return !data.hidden ? Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note__WEBPACK_IMPORTED_MODULE_2__["default"], _objectSpread({
                key: "note-".concat(data.id)
              }, props, {
                data
              })) : Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note_bubble__WEBPACK_IMPORTED_MODULE_3__["default"], _objectSpread({
                key: "note-".concat(data.id)
              }, props, {
                data
              }));
            }))];
          }
        }
      ),
      /***/
      "./src/components/react-sticky-notes/views/page-view.js": (
        /*!**************************************************************!*\
          !*** ./src/components/react-sticky-notes/views/page-view.js ***!
          \**************************************************************/
        /*! exports provided: PageView */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          __webpack_require__.d(__webpack_exports__, "PageView", function() {
            return PageView;
          });
          var _utils__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./../utils */
            "./src/components/react-sticky-notes/utils/index.js"
          );
          var _navbar__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
            /*! ./../navbar */
            "./src/components/react-sticky-notes/navbar/index.js"
          );
          var _partials_note_body__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
            /*! ../partials/note-body */
            "./src/components/react-sticky-notes/partials/note-body.js"
          );
          function _objectSpread(target) {
            for (var i = 1; i < arguments.length; i++) {
              var source = arguments[i] != null ? arguments[i] : {};
              var ownKeys = Object.keys(source);
              if (typeof Object.getOwnPropertySymbols === "function") {
                ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function(sym) {
                  return Object.getOwnPropertyDescriptor(source, sym).enumerable;
                }));
              }
              ownKeys.forEach(function(key) {
                _defineProperty(target, key, source[key]);
              });
            }
            return target;
          }
          function _defineProperty(obj, key, value) {
            if (key in obj) {
              Object.defineProperty(obj, key, { value, enumerable: true, configurable: true, writable: true });
            } else {
              obj[key] = value;
            }
            return obj;
          }
          function PageView(props) {
            return [Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_navbar__WEBPACK_IMPORTED_MODULE_1__["default"], _objectSpread({}, props, {
              key: "navbar"
            })), Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
              key: props.prefix,
              className: props.prefix,
              style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("container", props)
            }, props.items.map(function(data) {
              return Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])("div", {
                key: "note-".concat(data.id),
                className: "".concat(props.prefix, "--note"),
                style: Object(_utils__WEBPACK_IMPORTED_MODULE_0__["getElementStyle"])("note", _objectSpread({}, props, {
                  data
                }))
              }, Object(_utils__WEBPACK_IMPORTED_MODULE_0__["h"])(_partials_note_body__WEBPACK_IMPORTED_MODULE_2__["default"], _objectSpread({
                data
              }, props)));
            }))];
          }
        }
      ),
      /***/
      "./src/index.js": (
        /*!**********************!*\
          !*** ./src/index.js ***!
          \**********************/
        /*! exports provided: default */
        /***/
        function(module2, __webpack_exports__, __webpack_require__) {
          "use strict";
          __webpack_require__.r(__webpack_exports__);
          var _components_react_sticky_notes__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
            /*! ./components/react-sticky-notes */
            "./src/components/react-sticky-notes/index.js"
          );
          __webpack_exports__["default"] = _components_react_sticky_notes__WEBPACK_IMPORTED_MODULE_0__["default"];
        }
      ),
      /***/
      "react": (
        /*!************************!*\
          !*** external "react" ***!
          \************************/
        /*! no static exports found */
        /***/
        function(module2, exports2) {
          module2.exports = require_react();
        }
      )
      /******/
    });
  }
});
export default require_build();
/*! Bundled license information:

react/cjs/react.development.js:
  (** @license React v16.14.0
   * react.development.js
   *
   * Copyright (c) Facebook, Inc. and its affiliates.
   *
   * This source code is licensed under the MIT license found in the
   * LICENSE file in the root directory of this source tree.
   *)
*/
//# sourceMappingURL=@react-latest-ui_react-sticky-notes.js.map
