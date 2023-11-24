<template>
  <div class="parent">
    <h3 class="title">Lớp học hiện tại</h3>
    <div class="parent_group">
      <div v-if="$store.state.course.course" class="info_parent">
        <p>
          Khóa học :
          <strong>{{
            $store.getters["course/getbyid"](Number(this.$route.query.id))[0]
              .name
          }}</strong>
        </p>
        <p>
          Học phí :
          <strong
            >{{
              format_money(
                $store.getters["course/getbyid"](
                  Number(this.$route.query.id)
                )[0].price
              )
            }}/ lớp</strong
          >
        </p>
      </div>
      <div v-if="list_class_for_course.length > 0" class="boss-card">
        <div
          class="card"
          :id="'item' + item.id"
          v-for="item in list_class_for_course"
          :key="item.id"
        >
          <h3>{{ item.name }}</h3>
          <div class="card-context">
            <strong>CN</strong>
            <span v-if="$store.state.branch.branch"
              >Chi nhánh
              {{
                $store.getters["branch/getbyid"](item.branch_id)[0].name
              }}</span
            >
          </div>
          <div class="card-content">
            <div class="content-left">
              <h3 class="card-item">Ngày BD: {{ item.start_date }}</h3>
            </div>
            <div class="content-right">
              <h3 class="card-item">Ngày KT: {{ item.end_date }}</h3>
            </div>
          </div>
          <div v-if="cusclass" class="parent_btn">
            <button
              v-if="!cusclass_id($store.state.acount.authencation, item.id)"
              class="btn"
              @click="registerclass(item.id)"
            >
              Đăng ký lớp học
            </button>

            <button
              v-if="cusclass_id($store.state.acount.authencation, item.id)"
              style="opacity: 0.5"
              class="btn"
            >
              Đã đăng ký
            </button>
          </div>
        </div>
      </div>
      <div class="images" v-else>
        <img src="../../Imagess/notfound.png" alt="" />
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
import { useRoute } from "vue-router";
import { useToast } from "vue-toastification";
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
    const toast = useToast();
    store.dispatch("course/get_all");
    store.dispatch("class/get_all");
    store.dispatch("branch/get_all");
    store.dispatch("acount/get_all");
    store.dispatch("cus_class/get_all");
    store.state.acount.authencation = store.getters["acount/check_token"]();

    const this_page = ref(1);
    const limit_page = ref(4);

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
      if (store.state.class.class == null) {
        return [];
      }
      return store.state.class.class.filter(
        (e) => e.course_id == Number(useRoute().query.id)
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
          date_create: new Date().getTime(),
        });
        toast.success("Đăng ký thành công");
        store.dispatch("cus_class/add_cus", data);
      }
    };
    const cusclass = computed(() => {
      return store.state.cus_class.cus_class;
    });
    const cusclass_id = (cus, idclass) => {
      if (cus != null) {
        return store.getters["cus_class/getbyid"](cus.id, idclass);
      }
      return null;
    };
    const format_money = (value) => {
      return value.toLocaleString("vi-VN", {
        currency: "VND",
        style: "currency",
      });
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
      cusclass_id,
      format_money,
    };
  },
};
</script>
<style scoped>
.info_parent {
  text-align: start;
  padding: 20px;
  padding-bottom: 0;
  font-size: 18px;
}
.info_parent > p {
  margin-bottom: 5px;
}
.parent_btn {
  margin-top: 10px;
  text-align: end;
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
  min-height: 600px;
  min-width: 900px;
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
