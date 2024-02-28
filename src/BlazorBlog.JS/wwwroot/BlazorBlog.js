/*
    An instance of BlazorBlogJs is declared in the global scope at the bottom of this file. Any window event listeners should configured down there as well.

    Example:
    var bbjs = new BlazorBlogJs();
    window.addEventListener(`resize`, () => { bbjs.resize(); });
*/
class BlazorBlogJs {
    #inFade = 0.00;
    #outFade = 1.00;
    #viewWidth = 0;
    #rootFontSize = 0;

    /*
        The startup() function is called from the Blazor component's OnAfterRenderAsync() method.
        It fades in the header, main, and footer elements of the page.
    */
    startup() {
        this.#fadeIn(`header, main, footer`);
        return;

        setTimeout(() => {
            this.#fadeOut(`main, footer`);
        }, 2000);
    }

    /*
        The convertPixelsToRem() function converts rem units to pixels.
    */
    convertRemToPixels(rem) {
        console.debug(`convertRemToPixels(${rem})`);
        if (!rem) {
            throw new Error("The rem parameter is required.");
        }
        if (!rem.endsWith("rem")) {
            throw new Error("The rem parameter must end with 'rem'.");
        }
        const frem = rem.replace("rem", "");
        if (isNaN(frem)) {
            throw new Error("The rem parameter must be a number.");
        }
        return frem * this.getRootFontSize();
    }

    /*
        The getViewWidth() function returns the width of the current page.
    */
    getViewWidth(options) {
        if (this.#viewWidth === 0 || (options && options.reset)) {
            const header = document.querySelector(`header`);
            this.#viewWidth = parseFloat(header.offsetWidth);
        }
        return this.#viewWidth;
    }

    /*
        The getRootFontSize() function returns the root font size of the current page.
    */
    getRootFontSize(options) {
        if (this.#rootFontSize === 0 || (options && options.reset)) {
            const style = getComputedStyle(document.documentElement);
            const fontSize = style.fontSize;
            this.#rootFontSize = parseFloat(fontSize);
        }
        return this.#rootFontSize;
    }

    /*
        The getRootVariable() function returns the value of the specified CSS variable name defined in the `:root` element.
    */
    getRootVariable(varName) {
        const style = getComputedStyle(document.documentElement);
        return style.getPropertyValue(varName);
    }

    /*
        The resize() function is triggered by the window `resize` event.
        It resizes images and flex-boxes on the current page.

        This code is duplicated in EmbeddedMedia.razor and in FlexBox.razor.
        It exists as pure JavaScript here in order to easily bind it to the window `resize` event.
    */
    resize() {
        this.#viewWidth = this.getViewWidth({ reset: true });

        document.querySelectorAll(`[data-size]`).forEach(el => {

            const mediaSize = JSON.parse(el.getAttribute(`data-size`));
            const flexSizer = JSON.parse(el.getAttribute(`data-flex`));

            let scale = mediaSize.Scale;
            if (flexSizer && mediaSize.AltFlexScale) {
                if (this.#viewWidth < flexSizer.MinimumFlexWidth) {
                    scale = mediaSize.AltFlexScale;
                }
            }

            const maxWidth = scale * this.#viewWidth;
            const ratio = maxWidth / mediaSize.Width;

            let width = mediaSize.Width * ratio;
            let height = mediaSize.Height * ratio;

            el.setAttribute(`width`, width);
            el.setAttribute(`height`, height);

            if (el.tagName === `IMG`) {
                const borderRem = this.getRootVariable(`--Media_borderWidth`);
                const borderPx = 2 * this.convertRemToPixels(borderRem);
                width += borderPx;   // linkWidth
                height += borderPx;  // linkHeight
                el.parentElement.style.width = `${width}px`;
                el.parentElement.style.height = `${height}px`;
            }
        });

        document.querySelectorAll(`flex-box[data-flex]`).forEach(el => {
            const flexSizer = JSON.parse(el.getAttribute(`data-flex`));
            el.style.display = (this.#viewWidth < flexSizer.MinimumFlexWidth)
                ? "block" : "flex";
        });
    }


    #updateOpacity(qs, opacity) {
        document.querySelectorAll(qs).forEach(el => {
            el.style.opacity = opacity;
        });
    }

    #fadeIn(qs) {
        this.#updateOpacity(qs, this.#inFade);
        if (this.#inFade < 1) {
            this.#inFade += 0.1;
            requestAnimationFrame(() => this.#fadeIn(qs));
            return;
        }
        this.#inFade = 1;
        this.#updateOpacity(qs, this.#inFade);
        this.#inFade = 0;
    }

    #fadeOut(qs) {
        this.#updateOpacity(qs, this.#outFade);
        if (this.#outFade > 0) {
            this.#outFade -= 0.1;
            requestAnimationFrame(() => this.#fadeOut(qs));
            return;
        }
        this.#outFade = 0;
        this.#updateOpacity(qs, this.#outFade);
        this.#outFade = 1;
    }
}

/* *********************************** */
/* global instance and event listeners */
/* *********************************** */
var bbjs = new BlazorBlogJs();
window.addEventListener(`resize`, () => { bbjs.resize(); });


export { bbjs };