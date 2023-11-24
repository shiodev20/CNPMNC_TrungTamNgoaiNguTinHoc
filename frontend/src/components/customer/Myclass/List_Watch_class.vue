<template>
  <div class="parent">
    <h3 class="title">Lớp học của tôi</h3>
    <div class="parent_group">
      <div class="boss-card">
        <div
          class="card"
          :id="'item' + item.id"
          v-for="item in list_class_for_course"
          :key="item.id"
        >
          <h3>{{ classx(item.class_id).name }}</h3>
          <div class="card-context">
            <strong>CN</strong>
            <span>{{ branch(classx(item.class_id).branch_id).name }}</span>
          </div>
          <div class="card-content">
            <div class="content-left">
              <h3 class="card-item">
                Ngày BD: {{ classx(item.class_id).start_date }}
              </h3>
            </div>
            <div class="content-right">
              <h3 class="card-item">
                Ngày KT: {{ classx(item.class_id).end_date }}
              </h3>
            </div>
          </div>
          <div v-if="cusclass" class="parent_btn">
            <button class="btn">hóa đơn</button>
            <button
              v-if="item.status_pay"
              class="btn"
              @click="
                this.$router.push({
                  name: 'list-myclass',
                  query: { action: 'detail', id: item.class_id },
                })
              "
            >
              Xem lịch học
            </button>
            <button v-if="!item.status_pay" style="opacity: 0.5" class="btn">
              Chưa thanh toán
            </button>
          </div>
        </div>
      </div>

      <!-- pagnigation -->
      <div class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
      <!-- end pagnigation -->
    </div>
  </div>
  <basemodal v-if="$store.state.test.status_modal == true"></basemodal>
</template>
<script>
import modal from "@/components/common/Modal_Requid_loginV.vue";
import { computed, reactive, ref, watch } from "vue";
import store from "@/store";
export default {
  components: {
    basemodal: modal,
  },
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_class_for_course) {
      const div = document.getElementById(
        "item" + this.list_class_for_course[item].id
      );
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  setup() {
    store.dispatch("course/get_all");
    store.dispatch("class/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("acount/get_all");
    store.dispatch("class/get_all");
    store.dispatch("cus_class/get_all");
    store.state.acount.authencation = store.getters["acount/check_token"]();

    const this_page = ref(1);
    const limit_page = ref(6);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_class_for_course.value) {
        const div = document.getElementById(
          "item" + list_class_for_course.value[item].id
        );
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });
    const count_page = computed(() => {
      if (list_class_for_course.value == null) {
        return 0;
      }
      return Math.ceil(list_class_for_course.value.length / limit_page.value);
    });
    const chage = (item) => {
      this_page.value = item;
    };
    const list_class_for_course = computed(() => {
      if (store.state.cus_class.cus_class == null) {
        return [];
      }
      return store.getters["cus_class/getbyidcus"](
        store.state.acount.authencation.id
      );
    });
    const registerclass = (idclass) => {
      if (!localStorage.getItem("token")) {
        store.state.test.status_modal = true;
      } else {
        const data = reactive({
          customer_id: Number(store.state.acount.authencation.id),
          class_id: Number(idclass),
          status_pay: false,
        });
        store.dispatch("cus_class/add_cus", data);
      }
    };
    const cusclass = computed(() => {
      return store.state.cus_class.cus_class;
    });
    const branch = (value) => {
      return store.getters["branch/getbyid"](value)[0];
    };
    const classx = (value) => {
      return store.getters["class/getbyid"](value)[0];
    };
    const cusclass_id = (idcus, idclass) => {
      return store.getters["cus_class/getbyid"](idcus, idclass);
    };
    return {
      list_class_for_course,
      run_app,
      this_page,
      count_page,
      limit_page,
      chage,
      registerclass,
      cusclass,
      branch,
      classx,
      cusclass_id,
    };
  },
};
</script>
<style scoped>
.parent_btn {
  margin-top: 10px;
  text-align: end;
  display: flex;
  justify-content: space-between;
}
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
  gap: 10px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */

.card-context {
  display: flex;
  gap: 10px;
  padding: 10px 0;
  color: rgb(189, 4, 4);
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
.title {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
  margin-bottom: 17px;
}
.setting {
  padding: 20px;
  padding-top: 0px;
  text-align: end;
}
.Header {
  color: #071952;
  text-align: right;
  font-family: Roboto;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  margin-right: 30px;
}
.content-header {
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

.card-item {
  color: #000;
  font-family: Roboto;
  font-size: 14px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.price {
  color: rgb(197, 3, 3);
  font-family: Roboto;
  font-size: 16px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

p {
  margin: 0;
}

.btn {
  border: 2px solid #071952;
  color: #071952;
  padding: 10px 30px;
  border-radius: 10px;
  cursor: pointer;
}
.parent {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
}
.parent_group {
  background-color: white;
  border-radius: 20px;
  min-width: 800px;
  min-height: 600px;
}
.boss-card {
  display: grid;
  grid-template-columns: repeat(3, 300px);
  gap: 25px;
  border-radius: 10px;
  padding: 25px;
  padding-bottom: 60px;
  position: relative;
}
.image-card {
  width: 100%;
}

.card {
  position: relative;
  border-radius: 10px;
  background: #f7f7f7;
  overflow: hidden;
  padding: 20px 10px;
  text-align: start;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.card-content {
  display: grid;
  grid-template-columns: 1fr 1fr;
  align-items: center;
}
.card-content > div > h3 {
  margin: 5px 0;
}
h3 {
  margin: 0;
}
</style>
